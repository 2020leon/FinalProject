namespace FinalProject
{
    class JiaYu : Minion
    {
        public JiaYu() : base("高嘉瑜", 3, 5, 2, Attribute.Green) {

        }

        public override void Attack(Minion minion) {
            GameIO.GameOut.SendBeAttackedMinion(this, minion);
            minion.BeAttackedBy(this);
        }
    }
}