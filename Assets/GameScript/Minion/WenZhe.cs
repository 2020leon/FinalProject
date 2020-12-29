using System.Threading.Tasks;

namespace FinalProject
{
    class WenZhe : Minion
    {
        public WenZhe() : base("柯文哲", 3, 5, 2, Attribute.Other) {

        }

        public async override Task GoOnField() {
            await base.GoOnField();
            foreach (Minion minion in Player.MinionsOnField) {
                minion.IsEnabledToBeAttacked = false;
            }
        }
    }
}