using System.Threading.Tasks;

namespace FinalProject
{
    class XiKun : Minion
    {
        public XiKun() : base("游錫堃", 2, 5, 2, Attribute.Green) {

        }

        public override Task GoOnField() {
            base.GoOnField();
            ZhengChang zhengChang = null;
            ZongLi zongLi = null;
            foreach (Minion minion in Player.MinionsOnField) {
                if (minion is ZhengChang) {
                    zhengChang = minion as ZhengChang;
                }
                if (minion is ZongLi) {
                    zongLi = minion as ZongLi;
                }
            }
            if (zhengChang != null && zongLi != null) {
                YingWen yingWen = new YingWen() {
                    Player = this.Player
                };
                Player.MinionsOnField.Remove(zhengChang);
                zhengChang.RemoveFromField();
                Player.MinionsOnField.Remove(this);
                RemoveFromField();
                Player.MinionsOnField.Remove(zongLi);
                zongLi.RemoveFromField();
                Player.MinionsOnField.Add(yingWen);
                yingWen.GoOnField();
            }
            return Task.CompletedTask;
        }
    }
}
