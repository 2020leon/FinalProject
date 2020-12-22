using System;
using FinalProject;

namespace Example
{
    class GameOut : IGameOut
    {
        // Game.cs

        public void SendRoundCount(Game sender, int round) {
            Console.WriteLine("\n## Round " + round);
        }

        public void SendAfterStatus(Game sender, GameStatus gameStatus) {
        }

        public void SendGameStartSignal(Game sender) {
            Console.WriteLine("\n## Game start\n");
        }

        // Player.cs

        public void SendDrawnCard(Player sender, Card drawnCard) {
            Console.WriteLine("- " + sender.Name + " draws a card: " + drawnCard.Name);
        }

        public void SendAfterFieldSize(Player sender, short fieldSize) {
            Console.WriteLine("  - Field size after: " + sender.FieldSize);
        }

        public void SendAfterCash(Player sender, short cash) {
        }

        public void SendAfterIsInflation(Player sender, short inflationTime) {
            if (inflationTime > 0) {
                Console.WriteLine("  - " + sender.Name + " will spend extra " + Inflation.ExtraCost + " dollar.");
            }
            else {
                Console.WriteLine("\n- " + sender.Name + " will not suffer from inflation.");
            }
        }

        public void SendAfterStatus(Player sender, PlayerStatus playerStatus) {
        }

        public void SendStartingPlayerAndRound(Player sender, int gameRound) {
            Console.WriteLine("\n### " + sender.Name + "'s turn " + gameRound + "\n");
        }

        public void SendAfterWalletSize(Player sender, short walletSize) {
            Console.WriteLine("  - Wallet size after: " + walletSize);
        }

        public void SendPlayerCanDoNothingSignal(Player sender) {
            Console.WriteLine("- *" + sender.Name + " can do nothing.*");
        }

        public void SendHandOfPlayerIsFullSignal(Player sender) {
            Console.WriteLine("- *Hand is full.*");
        }

        public void SendDeckIsEmptySignal(Player sender) {
            Console.WriteLine("- *Deck is empty.*");
        }

        public void SendChoiceResponse(Player sender, Card chosenCard, ChoiceResponse response) {
            string msg;
            switch (response) {
                case ChoiceResponse.Attacked:
                    msg = "  - *" + chosenCard.Name + " has attacked, please choose another minion.*";
                    break;
                case ChoiceResponse.NoEnemyMinionCanBeAttacked:
                    msg = "  - *" + chosenCard.Name + " cannot attack anyone because they are disabled to be attacked.*";
                    break;
                case ChoiceResponse.AttackAfterPuttingMinionsOnField:
                    msg = "  - *You have put cards on field, you cannot choose a minion in hand to attack.*";
                    break;
                case ChoiceResponse.FieldIsFull:
                    msg = "  - *Field is full, you cannot put " + chosenCard.Name + " on field.*";
                    break;
                case ChoiceResponse.DisabledToBeAttacked:
                    msg = "  - *" + chosenCard.Name + " is disabled to be attack.*";
                    break;
                case ChoiceResponse.LackOfCash:
                    if (chosenCard is Minion){
                        msg = "  - *You have no enough cash, you cannot put " + chosenCard.Name + " on field.*";
                    }
                    else {
                        msg = "  - *You have no enough cash, you cannot launch " + chosenCard.Name + ".*";
                    }
                    break;
                default:
                    if (chosenCard is Player) {
                        msg = "  - " + sender.Name + " chooses a player: " + chosenCard.Name;
                    }
                    else {
                        msg = "  - " + sender.Name + " chooses a card: " + chosenCard.Player.Name + "'s " + chosenCard.Name;
                    }
                    break;
            }
            Console.WriteLine(msg);
        }

        public void SendEndOfRoundSignal(Player sender) {
            Console.WriteLine("- " + sender.Name + " end the round.");
            Console.Write("\n> - Cards in hand:");
            foreach (Card card in sender.CardsInHand) {
                Console.Write(" " + card.Name);
            }
            Console.Write("\n> - Cards on field:");
            foreach (Minion minion in sender.MinionsOnField) {
                Console.Write(" " + minion.Name + $"({minion.Hp})");
            }
            Console.WriteLine();
        }

        // Minion.cs

        public void SendAfterHp(Minion sender, short hp) {
            Console.WriteLine("  - " + sender.Name + " Hp after: " + hp);
        }

        public void SendAfterAtk(Minion sender, short atk) {
            Console.WriteLine("  - " + sender.Name + " Atk after: " + atk);
        }

        public void SendAfterIsEnabledToBeAttacked(Minion sender, bool isEnabledToBeAttacked) {
            if (isEnabledToBeAttacked) {
                Console.WriteLine("- " + sender.Name + " is now enabled to be attacked.");
            }
            else {
                Console.WriteLine("  - " + sender.Name + " is disabled to be attacked.");
            }
        }

        public void SendBeAttackedMinion(Minion sender, Minion beAttackedMinion) {
            Console.WriteLine("- " + sender.Player.Name + "'s " + sender.Name + $"({sender.Hp}) attacks " + beAttackedMinion.Name + $"({beAttackedMinion.Hp}).");
            if (sender is JiaYu) {
                Console.WriteLine("  - " + sender.Name + " will not be attacked.");
            }
        }

        public void SendRemoveFromFieldSignal(Minion sender) {
        }

        public void SendMinionDieSignal(Minion sender) {
            Console.WriteLine("  - " + sender.Player.Name + "'s " + sender.Name + " died.");
        }

        public void SendPlayerDieSignal(Player sender) {
            Console.WriteLine("  - " + sender.Name + " died.");
        }

        public void SendGoOnFieldSignal(Minion sender) {
            Console.WriteLine("- " + sender.Player.Name + "'s " + sender.Name + " goes on field.");
        }

        // Spell.cs

        public void SendLaunchSpellSignal(Spell sender) {
            Console.WriteLine("- " + sender.Player.Name + " launches a spell: " + sender.Name);
        }

        // Minion/ShiZhong.cs

        public void SendRemainingMasks(ShiZhong sender, short remainingMasks) {
            System.Console.WriteLine("  - " + sender.Player.Name + "'s " + sender.Name + " has remaining " + remainingMasks + " masks, he won't be attacked.");
        }
    }
}
