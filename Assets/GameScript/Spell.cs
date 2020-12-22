namespace FinalProject
{
    abstract class Spell : Card
    {
        protected Spell(string name, short cost) : base(name, cost) {

        }

        public virtual void LaunchSpell() {
            GameIO.GameOut.SendLaunchSpellSignal(this);
        }
    }
}