using System.Threading.Tasks;

namespace FinalProject
{
    class FakeNews : Spell
    {
        public FakeNews() : base("假新聞", 2) {

        }

        public async override Task LaunchSpell() {
            await base.LaunchSpell();
            Player enemy = Player.Game.GetMyEnemy(Player);
            if (enemy.MinionsOnField.Count == 0) {
                // output something
            }
            else {
                Minion minion;
                while (!enemy.MinionsOnField.Contains(minion = await Player.ChooseEnemyMinionOnField(enemy))) {
                    GameIO.GameOut.SendChoiceResponse(Player, minion, ChoiceResponse.NotCardOnEnemyField);
                }
                GameIO.GameOut.SendChoiceResponse(Player, minion, ChoiceResponse.Success);
                minion.Hp -= 5;
                if (minion.IsDead) {
                    enemy.MinionsOnField.Remove(minion);
                    minion.Die();
                }
            }
        }
    }
}