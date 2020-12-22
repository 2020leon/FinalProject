namespace FinalProject
{
    class FakeNews : Spell
    {
        public FakeNews() : base("假新聞", 2) {

        }

        public override void LaunchSpell() {
            base.LaunchSpell();
            Player enemy = Player.Game.GetMyEnemy(Player);
            if (enemy.MinionsOnField.Count == 0) {
                // output something
            }
            else {
                Minion minion = Player.ChooseEnemyMinionOnField(enemy);
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