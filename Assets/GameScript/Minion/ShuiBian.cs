using System.Threading.Tasks;

namespace FinalProject
{
    class ShuiBian : Minion
    {
        public ShuiBian() : base("陳水扁", 3, 3, 1, Attribute.Green) {

        }

        public override Task GoOnField() {
            base.GoOnField();
            Player.DrawAndAdd();
            return Task.CompletedTask;
        }
    }
}
