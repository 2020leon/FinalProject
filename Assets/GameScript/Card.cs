namespace FinalProject
{
    abstract class Card
    {
        private readonly string name;
        private short cost;
        private Player player;

        public string Name {
            get { return name; }
        }

        public short Cost {
            get { return cost; }
        }

        public Player Player {
            internal set { player = value; }
            get { return player; }
        }

        protected Card(string name, short cost) {
            this.cost = cost;
            this.name = name;
            this.player = null;
        }
    }
}
