using System.Threading.Tasks;

namespace FinalProject
{
    class ZongLi : Minion
    {
        public ZongLi() : base("許宗力", 2, 5, 2, Attribute.Other) {

        }

        public async override Task GoOnField() {
            await base.GoOnField();
            ZhengChang zhengChang = null;
            XiKun xiKun = null;
            foreach (Minion minion in Player.MinionsOnField) {
                if (minion is ZhengChang) {
                    zhengChang = minion as ZhengChang;
                }
                if (minion is XiKun) {
                    xiKun = minion as XiKun;
                }
            }
            if (zhengChang != null && xiKun != null) {
                YingWen yingWen = new YingWen() {
                    Player = this.Player
                };
                Player.MinionsOnField.Remove(zhengChang);
                zhengChang.RemoveFromField();
                Player.MinionsOnField.Remove(xiKun);
                xiKun.RemoveFromField();
                Player.MinionsOnField.Remove(this);
                RemoveFromField();
                Player.MinionsOnField.Add(yingWen);
                await yingWen.GoOnField();
            }
        }
    }
}