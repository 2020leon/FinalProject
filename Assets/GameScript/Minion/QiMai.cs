namespace FinalProject
{
    class QiMai : Minion
    {
        public QiMai() : base("陳其邁", 5, 5, 1, Attribute.Green) {

        }

        public override void GoOnField() {
            base.GoOnField();
            Player enemy = Player.Game.GetMyEnemy(Player);
            if (!(Player.IsFieldFull || enemy.MinionsOnField.Count == 0)) {
                Minion minion = Player.ChooseEnemyMinionOnField(enemy);
                GameIO.GameOut.SendChoiceResponse(Player, minion, ChoiceResponse.Success);
                enemy.MinionsOnField.Remove(minion);
                minion.RemoveFromField();
                minion.Player = Player;
                Player.MinionsOnField.Add(minion);
                minion.GoOnField();
            }
        }
    }
}