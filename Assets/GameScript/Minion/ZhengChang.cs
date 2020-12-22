namespace FinalProject
{
    class ZhengChang : Minion
    {
        public ZhengChang() : base("蘇貞昌", 2, 5, 2, Attribute.Green) {

        }

        public override void GoOnField() {
            base.GoOnField();
            XiKun xiKun = null;
            ZongLi zongLi = null;
            foreach (Minion minion in Player.MinionsOnField) {
                if (minion is XiKun) {
                    xiKun = minion as XiKun;
                }
                if (minion is ZongLi) {
                    zongLi = minion as ZongLi;
                }
            }
            if (xiKun != null && zongLi != null) {
                YingWen yingWen = new YingWen() {
                    Player = this.Player
                };
                Player.MinionsOnField.Remove(this);
                RemoveFromField();
                Player.MinionsOnField.Remove(xiKun);
                xiKun.RemoveFromField();
                Player.MinionsOnField.Remove(zongLi);
                zongLi.RemoveFromField();
                Player.MinionsOnField.Add(yingWen);
                yingWen.GoOnField();
            }
        }
    }
}