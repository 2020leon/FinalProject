using System.Threading.Tasks;

namespace FinalProject
{
    class GuoYu : Minion
    {
        public GuoYu() : base("韓國瑜", 2, 3, 1, Attribute.Blue) {

        }

        public async override Task GoOnField() {
            await base.GoOnField();
            Player.WalletSize++;
        }

        public override void RemoveFromField() {
            base.RemoveFromField();
            Player.WalletSize--;
        }
    }
}