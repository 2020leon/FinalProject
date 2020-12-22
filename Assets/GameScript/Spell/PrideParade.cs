namespace FinalProject
{
    class PrideParade : Spell
    {
        public PrideParade() : base("同志遊行", 1) {

        }

        public override void LaunchSpell() {
            base.LaunchSpell();
            foreach (Minion minion in Player.MinionsOnField) {
                if (minion.Attribute == Attribute.Green) {
                    minion.Hp++;
                }
            }
        }
    }
}