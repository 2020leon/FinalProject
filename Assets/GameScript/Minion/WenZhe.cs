using System.Threading.Tasks;

namespace FinalProject
{
    class WenZhe : Minion
    {
        public WenZhe() : base("柯文哲", 3, 5, 2, Attribute.Other) {

        }

        public override Task GoOnField() {
            base.GoOnField();
            foreach (Minion minion in Player.MinionsOnField) {
                minion.IsEnabledToBeAttacked = false;
            }
            return Task.CompletedTask;
        }
    }
}
