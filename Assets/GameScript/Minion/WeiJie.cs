namespace FinalProject
{
    class WeiJie : Minion
    {
        public WeiJie() : base("邱威傑", 3, 5, 1, Attribute.Other) {

        }

        public override void Attack(Minion minion) {
            base.Attack(minion);
            Atk++;
        }
    }
}