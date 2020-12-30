using System.Threading.Tasks;

namespace FinalProject
{
    class QiMai : Minion
    {
        public QiMai() : base("陳其邁", 5, 5, 1, Attribute.Green) {

        }

        public async override Task GoOnField() {
            await base.GoOnField();
            Player enemy = Player.Game.GetMyEnemy(Player);
            if (!(Player.IsFieldFull || enemy.MinionsOnField.Count == 0)) {
                Minion minion;
                while (!enemy.MinionsOnField.Contains(minion = await Player.ChooseEnemyMinionOnField(enemy))) {
                    GameIO.GameOut.SendChoiceResponse(Player, minion, ChoiceResponse.NotCardOnEnemyField);
                }
                GameIO.GameOut.SendChoiceResponse(Player, minion, ChoiceResponse.Success);
                enemy.MinionsOnField.Remove(minion);
                minion.RemoveFromField();
                minion.Player = Player;
                Player.MinionsOnField.Add(minion);
                await minion.GoOnField();
            }
        }
    }
}