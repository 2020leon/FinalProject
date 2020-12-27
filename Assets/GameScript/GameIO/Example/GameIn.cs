using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FinalProject;

namespace Example
{
    class GameIn : IGameIn
    {
        // Game.cs

        [Obsolete("Deprecated", true)]
        public async Task<string> ReceiveNameOfPlayerAsync(Game receiver) {
            await Task.Delay(0);
            return "Player";
        }

        // Player.cs

        public async Task<Minion> ReceiveEnemyMinionOnFieldAsync(Player receiver, Player enemy) {
            Random random = new Random();
            await Task.Delay(0);
            return enemy.MinionsOnField[random.Next(enemy.MinionsOnField.Count)];
        }

        public async Task<Minion> ReceiveEnemyHeadOrMinionOnFieldAsync(Player receiver, Player enemy) {
            await Task.Delay(0);
            if (enemy.MinionsOnField.Count == 0) {
                return enemy;
            }
            else {
                Random random = new Random();
                return enemy.MinionsOnField[random.Next(enemy.MinionsOnField.Count)];
            }
        }

        public async Task<Card> ReceiveMyCardAsync(Player receiver, Player enemy) {
            Random random = new Random();
            await Task.Delay(0);
            if (
                receiver.MinionsOnField.Count > 0 &&
                receiver.Status != PlayerStatus.PuttingMinionsToField &&
                !receiver.IsAllMinionsOnFieldHaveAttacked && (
                    enemy.MinionsOnField.Count == 0 ||
                    (enemy.MinionsOnField.Count > 0 && enemy.CanAnyMinionAttack)
                )
            ) {
                var yetAttackedMinions = new HashSet<Minion>(receiver.MinionsOnField);
                yetAttackedMinions.ExceptWith(receiver.AttackedMinions);
                return new List<Minion>(yetAttackedMinions)[random.Next(yetAttackedMinions.Count)];
            }
            else {
                var cards = new List<Card>();
                foreach (var card in receiver.CardsInHand) {
                    if (
                        card.Cost + Inflation.ExtraCost * receiver.InflationTime <= receiver.Cash &&
                        ((receiver.IsFieldFull && card is Spell) || !receiver.IsFieldFull)
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
}