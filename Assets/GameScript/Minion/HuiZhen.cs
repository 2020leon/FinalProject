namespace FinalProject
{
    class HuiZhen : Minion
    {
        public HuiZhen() : base("蘇慧貞", 2, 5, 1, Attribute.Other) {

        }

        public override void GoOnField() {
            base.GoOnField();
            Player.FieldSize += 2;
        }

        public override void RemoveFromField() {
            base.RemoveFromField();
            Player.FieldSize -= 2;
        }
    }
}