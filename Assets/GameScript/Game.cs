using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProject
{
    enum GameStatus
    {
        PlayerWin, ComputerWin, Tie, PlayersTurn, ComputersTurn, Start, Continue
    }

    class Game
    {
        private static short timeOfDrawWhenStart = 3;
        private int round;
        private readonly Player player;
        private readonly Player computer;
        private readonly List<Card> deck;
        private GameStatus status;

        public int Round {
            get { return round; }
        }

        public List<Card> Deck {
            get { return deck; }
        }

        public GameStatus Status {
            get { return status; }
            private set {
                status = value;
                GameIO.GameOut.SendAfterStatus(this, status);
            }
        }

        public Game() {
            round = 0;
            player = new Player(this, "Player", true);
            computer = new Player(this, "Computer", false);
            deck = new List<Card>();
            Status = GameStatus.Start;
        }

        public void Start() {
            GameIO.GameOut.SendGameStartSignal(this);
            for (int i = 0; i < 3; i++) {
                deck.Add(new ZhengChang());
                deck.Add(new XiKun());
                deck.Add(new ZongLi());
                deck.Add(new ShiZhong());
                deck.Add(new GuoYu());
                deck.Add(new QiMai());
                deck.Add(new WenZhe());
                deck.Add(new JiaYu());
                deck.Add(new YingJiu());
                deck.Add(new ShuiBian());
                deck.Add(new YouYi());
                deck.Add(new ShiJian());
                deck.Add(new WeiJie());
                deck.Add(new HuiZhen());
                deck.Add(new PrideParade());
                deck.Add(new CtiNews());
                deck.Add(new Earthquake());
                deck.Add(new Inflation());
                deck.Add(new FakeNews());
            }
            for (int i = 0; i < timeOfDrawWhenStart; i++) {
                player.DrawAndAdd();
                computer.DrawAndAdd();
            }
        }

        public async Task DoRounds() {
            do {
                await DoRound();
            } while (Status == GameStatus.Continue);
        }

        private async Task DoRound() {
            round++;
            GameIO.GameOut.SendRoundCount(this, Round);
            Status = GameStatus.PlayersTurn;
            player.Cash += (short)Round;
            await player.DoRound(computer);
            if (player.Status == PlayerStatus.Victory) {
                Status = GameStatus.PlayerWin;
                return;
            }
            if (computer.CanDoNothing && player.CanDoNothing) {
                Status = GameStatus.Tie;
                return;
            }
            Status = GameStatus.ComputersTurn;
            computer.Cash += (short)Round;
            await computer.DoRound(player);
            if (computer.Status == PlayerStatus.Victory) {
                Status = GameStatus.ComputerWin;
                return;
            }
            if (player.CanDoNothing && computer.CanDoNothing) {
                Status = GameStatus.Tie;
                return;
            }
            Status = GameStatus.Continue;
        }

        public Player GetMyEnemy(Player me) {
            if (me == player) return computer;
            if (me == computer) return player;
            return null;
        }
    }
}
