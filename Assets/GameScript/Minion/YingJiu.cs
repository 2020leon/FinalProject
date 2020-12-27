namespace FinalProject
{
    class YingJiu : Minion
    {
        public YingJiu() : base("馬英九", 3, 5, 1, Attribute.Blue) {

        }

        public override void Attack(Minion minion) {
            base.Attack(minion);
            if (minion.Attribute == Attribute.Green && minion.IsEnabledToBeAttacked) {
                minion.BeAttackedInAddition(2);
            }
        }
    }
}
