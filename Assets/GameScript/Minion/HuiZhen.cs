using System.Threading.Tasks;

namespace FinalProject
{
    class HuiZhen : Minion
    {
        public HuiZhen() : base("蘇慧貞", 2, 5, 1, Attribute.Other) {

        }

        public async override Task GoOnField() {
            await base.GoOnField();
            Player.FieldSize += 2;
        }

        public override void RemoveFromField() {
            base.RemoveFromField();
            Player.FieldSize -= 2;
        }
    }
}