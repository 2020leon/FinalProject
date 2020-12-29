namespace FinalProject
{
    class ShiJian : Minion
    {
        public ShiJian() : base("王世堅", 3, 5, 1, Attribute.Green) {

        }

        public override void Die() {
            base.Die();
            foreach (Minion minion in Player.MinionsOnField) {
                minion.Atk += 2;
            }
        }
    }
}