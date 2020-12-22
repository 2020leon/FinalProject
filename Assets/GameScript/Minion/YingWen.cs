namespace FinalProject
{
    class YingWen : Minion
    {
        internal override short NumberOfAttack {
            get { return 2; }
        }

        public YingWen() : base("蔡英文", 0, 30, 10, Attribute.Green) {

        }
    }
}