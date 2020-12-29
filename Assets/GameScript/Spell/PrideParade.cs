using System.Threading.Tasks;

namespace FinalProject
{
    class PrideParade : Spell
    {
        public PrideParade() : base("同志遊行", 1) {

        }

        public async override Task LaunchSpell() {
            await base.LaunchSpell();
            foreach (Minion minion in Player.MinionsOnField) {
                if (minion.Attribute == Attribute.Green) {
                    minion.Hp++;
                }
            }
        }
    }
}