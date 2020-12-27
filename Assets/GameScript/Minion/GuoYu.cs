using System.Threading.Tasks;

namespace FinalProject
{
    class GuoYu : Minion
    {
        public GuoYu() : base("韓國瑜", 2, 3, 1, Attribute.Blue) {

        }

        public override Task GoOnField() {
            base.GoOnField();
            Player.WalletSize++;
            return Task.CompletedTask;
        }

        public override void RemoveFromField() {
            base.RemoveFromField();
            Player.WalletSize--;
        }
    }
}
