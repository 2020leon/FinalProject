namespace FinalProject
{
    class Earthquake : Spell
    {
        public Earthquake() : base("大地震", 3) {

        }

        public override void LaunchSpell() {
            base.LaunchSpell();
            var beAttackedMinions = new System.Collections.Generic.HashSet<Minion>();
            var enemy = Player.Game.GetMyEnemy(Player);
            foreach (var minion in Player.MinionsOnField) {
                beAttackedMinions.Add(minion);
            }
            foreach (var minion in enemy.MinionsOnField) {
                beAttackedMinions.Add(minion);
            }
            foreach (var beAttackedMinion in beAttackedMinions) {
                beAttackedMinion.Hp -= 5;
                if (beAttackedMinion.IsDead) {
                    beAttackedMinion.Player.MinionsOnField.Remove(beAttackedMinion);
                    beAttackedMinion.Player.AttackedMinions.Remove(beAttackedMinion);
                    beAttackedMinion.Die();
                }
            }
            beAttackedMinions.Clear();
        }
    }
}