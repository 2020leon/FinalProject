using System.Threading.Tasks;

namespace FinalProject
{
    abstract class Spell : Card
    {
        protected Spell(string name, short cost) : base(name, cost) {

        }

        public virtual Task LaunchSpell() {
            GameIO.GameOut.SendLaunchSpellSignal(this);
            return Task.CompletedTask;
        }
    }
}
