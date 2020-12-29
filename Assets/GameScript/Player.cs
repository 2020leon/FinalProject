using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProject
{
    enum PlayerStatus
    {
        Waiting, Setup, Drawing, Acting, PuttingMinionsToField, Victory
    }

    class Player : Minion
    {
        private short walletSize;
        private short fieldSize;
        private short handSize;
        private short cash;
        private short inflationTime;
        private PlayerStatus status;
        private readonly List<Card> cardsInHand;
        private readonly List<Minion> minionsOnField;
        private readonly HashSet<Minion> attackedMinions;
        private readonly Game game;
        private readonly bool isManual;

        public short WalletSize {
            internal set {
                walletSize = value;
                GameIO.GameOut.SendAfterWalletSize(this, walletSize);
                Cash = Cash;
            }
            get { return walletSize; }
        }

        public short FieldSize {
            internal set {
                fieldSize = value;
                GameIO.GameOut.SendAfterFieldSize(this, fieldSize);
            }
            get { return fieldSize; }
        }

        public short Cash {
            set {
                if (value <= walletSize) cash = value;
                else cash = walletSize;
                GameIO.GameOut.SendAfterCash(this, cash);
            }
            get { return cash; }
        }

        internal short InflationTime {
            set {
                inflationTime = value;
                GameIO.GameOut.SendAfterIsInflation(this, inflationTime);
            }
            get { return inflationTime; }
        }

        public PlayerStatus Status {
            private set {
                status = value;
                GameIO.GameOut.SendAfterStatus(this, status);
            }
            get { return status; }
        }

        public List<Card> CardsInHand {
            get { return cardsInHand; }
        }

        public List<Minion> MinionsOnField {
            get { return minionsOnField; }
        }

        public HashSet<Minion> AttackedMinions {
            get { return attackedMinions; }
        }

        public Game Game {
            get { return game; }
        }

        public bool IsFieldFull {
            get { return minionsOnField.Count >= fieldSize; }
        }

        public bool IsHandFull {
            get { return cardsInHand.Count >= handSize; }
        }

        public bool CanDoNothing {
            get { return game.Deck.Count == 0 && minionsOnField.Count == 0 && cardsInHand.Count == 0; }
        }

        public bool IsAllMinionsOnFieldHaveAttacked {
            get { return minionsOnField.Count == attackedMinions.Count; }
        }

        public bool CanAnyMinionAttack {
            get {
                foreach (Minion minion in minionsOnField) {
                    if (minion.IsEnabledToBeAttacked) {
                        return true;
                    }
                }
                return false;
            }
        }

        public Player(Game game, string name, bool isManual) : base(name, 0, 10, 0, Attribute.Other) {
            walletSize = 5;
            fieldSize = 5;
            handSize = 5;
            cash = 0;
            inflationTime = 0;
            status = PlayerStatus.Waiting;
            cardsInHand = new List<Card>();
            minionsOnField = new List<Minion>();
            attackedMinions = new HashSet<Minion>();
            this.game = game;
            this.isManual = isManual;
        }

        public async Task DoRound(Player other) {
            GameIO.GameOut.SendStartingPlayerAndRound(this, game.Round);
            Status = PlayerStatus.Setup;
            Setup();
            if (CanDoNothing) {
                GameIO.GameOut.SendPlayerCanDoNothingSignal(this);
            }
            else {
                Status = PlayerStatus.Drawing;
                DrawAndAdd();
                Status = PlayerStatus.Acting;
                await Act(other);
            }
            if (status != PlayerStatus.Victory) {
                EndRound();
                Status = PlayerStatus.Waiting;
            }
        }

        private void EndRound() {
            attackedMinions.Clear();
            if (inflationTime > 0) InflationTime = 0;
        }

        private void Setup() {
            foreach (Minion minion in minionsOnField) {
                if (!minion.IsEnabledToBeAttacked) {
                    minion.IsEnabledToBeAttacked = true;
                }
            }
        }

        public Card DrawAndAdd() {
            Random random = new Random();
            Card card = null;
            if (IsHandFull) {
                GameIO.GameOut.SendHandOfPlayerIsFullSignal(this);
            }
            else if (game.Deck.Count == 0) {
                GameIO.GameOut.SendDeckIsEmptySignal(this);
            }
            else {
                int index = random.Next(game.Deck.Count);
                card = game.Deck[index];
                game.Deck.RemoveAt(index);
                card.Player = this;
                cardsInHand.Add(card);
                GameIO.GameOut.SendDrawnCard(this, card);
            }
            return card;
        }

        private async Task Act(Player other) {
            Card chosenCard;
            while ((chosenCard = await ChooseMyCard(other)) != null) {
                if (chosenCard is Minion) {
                    Minion chosenMinion = chosenCard as Minion;
                    if (minionsOnField.Contains(chosenMinion)) {
                        if (status != PlayerStatus.PuttingMinionsToField) {
                            if (attackedMinions.Contains(chosenMinion)) {
                                GameIO.GameOut.SendChoiceResponse(this, chosenMinion, ChoiceResponse.Attacked);
                            }
                            else if (other.MinionsOnField.Count > 0 && !other.CanAnyMinionAttack) {
                                GameIO.GameOut.SendChoiceResponse(this, chosenMinion, ChoiceResponse.NoEnemyMinionCanBeAttacked);
                            }
                            else {
                                GameIO.GameOut.SendChoiceResponse(this, chosenMinion, ChoiceResponse.Success);
                                await DoAttack(chosenMinion, other);
                                if (status == PlayerStatus.Victory) {
                                    return;
                                }
                            }
                        }
                        else { // cannot Attack
                            GameIO.GameOut.SendChoiceResponse(this, chosenMinion, ChoiceResponse.AttackAfterPuttingMinionsOnField);
                        }
                    }
                    else { // in hand
                        if (chosenMinion.Cost + Inflation.ExtraCost * inflationTime <= cash && !IsFieldFull) {
                            GameIO.GameOut.SendChoiceResponse(this, chosenMinion, ChoiceResponse.Success);
                            Status = PlayerStatus.PuttingMinionsToField;
                            await PutMinionInHandToField(chosenMinion);
                        }
                        else if (IsFieldFull) {
                            GameIO.GameOut.SendChoiceResponse(this, chosenMinion, ChoiceResponse.FieldIsFull);
                        }
                        else {
                            GameIO.GameOut.SendChoiceResponse(this, chosenMinion, ChoiceResponse.LackOfCash);
                        }
                    }
                }
                else { // is Spell
                    Spell chosenSpell = chosenCard as Spell;
                    if (chosenSpell.Cost + Inflation.ExtraCost * inflationTime <= cash) {
                        GameIO.GameOut.SendChoiceResponse(this, chosenSpell, ChoiceResponse.Success);
                        await LaunchSpell(chosenSpell);
                    }
                    else {
                        GameIO.GameOut.SendChoiceResponse(this, chosenSpell, ChoiceResponse.LackOfCash);
                    }
                }
            }
            GameIO.GameOut.SendEndOfRoundSignal(this);
        }

        private async Task DoAttack(Minion attacker, Player other) {
            for (
                int i = 0;
                i < attacker.NumberOfAttack && (
                    other.MinionsOnField.Count == 0 || (other.MinionsOnField.Count > 0 && other.CanAnyMinionAttack)
                );
                i++
            ) {
                Minion enemyMinion;
                do {
                    enemyMinion = await ChooseEnemyHeadOrMinionOnField(other);
                    if (!enemyMinion.IsEnabledToBeAttacked || (other.MinionsOnField.Count > 0 && enemyMinion is Player)) {
                        GameIO.GameOut.SendChoiceResponse(this, enemyMinion, ChoiceResponse.DisabledToBeAttacked);
                    }
                } while (!enemyMinion.IsEnabledToBeAttacked || (other.MinionsOnField.Count > 0 && enemyMinion is Player));
                GameIO.GameOut.SendChoiceResponse(this, enemyMinion, ChoiceResponse.Success);
                attacker.Attack(enemyMinion);
                attackedMinions.Add(attacker);
                if (attacker.IsDead) {
                    minionsOnField.Remove(attacker);
                    attackedMinions.Remove(attacker);
                    attacker.Die();
                }
                if (enemyMinion.IsDead) {
                    other.MinionsOnField.Remove(enemyMinion);
                    enemyMinion.Die();
                    if (enemyMinion is Player) {
                        Status = PlayerStatus.Victory;
                        break;
                    }
                }
            }
        }

        private async Task PutMinionInHandToField(Minion minion) {
            cardsInHand.Remove(minion);
            Cash -= (short)(minion.Cost + Inflation.ExtraCost * inflationTime);
            minionsOnField.Add(minion);
            await minion.GoOnField();
        }

        private async Task LaunchSpell(Spell spell) {
            cardsInHand.Remove(spell);
            await spell.LaunchSpell();
        }

        private async Task<Card> ChooseMyCard(Player other) {
            if (isManual) {
                return await GameIO.GameIn.ReceiveMyCardAsync(this, other);
            }
            else {
                Random random = new Random();
                if (
                    minionsOnField.Count > 0 &&
                    status != PlayerStatus.PuttingMinionsToField &&
                    !IsAllMinionsOnFieldHaveAttacked && (
                        other.MinionsOnField.Count == 0 ||
                        (other.MinionsOnField.Count > 0 && other.CanAnyMinionAttack)
                    )
                ) {
                    var yetAttackedMinions = new HashSet<Minion>(minionsOnField);
                    yetAttackedMinions.ExceptWith(attackedMinions);
                    return new List<Minion>(yetAttackedMinions)[random.Next(yetAttackedMinions.Count)];
                }
                else {
                    var cards = new List<Card>();
                    foreach (var card in cardsInHand) {
                        if (
                            card.Cost + Inflation.ExtraCost * inflationTime <= cash &&
                            ((IsFieldFull && card is Spell) || !IsFieldFull)
                        ) {
                            cards.Add(card);
                        }
                    }
                    if (cards.Count > 0) {
                        return cards[random.Next(cards.Count)];
                    }
                    else return null;
                }
            }
        }

        private async Task<Minion> ChooseEnemyHeadOrMinionOnField(Player other) {
            if (isManual) {
                return await GameIO.GameIn.ReceiveEnemyHeadOrMinionOnFieldAsync(this, other);
            }
            else {
                if (other.MinionsOnField.Count == 0) {
                    return this;
                }
                else {
                    Random random = new Random();
                    return other.MinionsOnField[random.Next(other.MinionsOnField.Count)];
                }
            }
        }

        public async Task<Minion> ChooseEnemyMinionOnField(Player other) {
            if (other.MinionsOnField.Count == 0) return null;
            if (isManual) {
                return await GameIO.GameIn.ReceiveEnemyMinionOnFieldAsync(this, other);
            }
            else {
                Random random = new Random();
                return other.MinionsOnField[random.Next((int)other.MinionsOnField.Count)];
            }
        }
    }
}
