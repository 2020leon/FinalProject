using System.Threading.Tasks;

namespace FinalProject
{
    class Inflation : Spell
    {
        public static short ExtraCost {
            get { return 1; }
        }
        public Inflation() : base("通貨膨脹", 3) {

        }

        public async override Task LaunchSpell() {
            await base.LaunchSpell();
            Player.Game.GetMyEnemy(Player).InflationTime++;
        }
    }
}