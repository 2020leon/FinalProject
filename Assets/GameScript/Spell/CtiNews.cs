using System.Threading.Tasks;

namespace FinalProject
{
    class CtiNews : Spell
    {
        public CtiNews() : base("中天新聞", 2) {

        }

        public override Task LaunchSpell() {
            base.LaunchSpell();
            var beAttackedMinions = new System.Collections.Generic.HashSet<Minion>();
            var enemy = Player.Game.GetMyEnemy(Player);
            foreach (var minion in Player.MinionsOnField) {
                if (minion is YingWen) {
                    beAttackedMinions.Add(minion);
                }
            }
            foreach (var minion in enemy.MinionsOnField) {
                if (minion is YingWen) {
                    minion.Hp /= 2;
                    beAttackedMinions.Add(minion);
                }
            }
            foreach (var beAttackedMinion in beAttackedMinions) {
                beAttackedMinion.Hp /= 2;
                if (beAttackedMinion.IsDead) {
                    beAttackedMinion.Player.MinionsOnField.Remove(beAttackedMinion);
                    beAttackedMinion.Player.AttackedMinions.Remove(beAttackedMinion);
                    beAttackedMinion.Die();
                }
            }
            beAttackedMinions.Clear();
            return Task.CompletedTask;
        }
    }
}