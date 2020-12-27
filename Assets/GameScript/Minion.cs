using System.Threading.Tasks;

namespace FinalProject
{
    enum Attribute {
        Green, Blue, Other
    }

    abstract class Minion : Card
    {
        private short hp;
        private short atk;
        private bool isEnableToBeAttacked;
        private readonly Attribute attribute;

        public short Hp {
            get { return hp; }
            set {
                hp = value;
                GameIO.GameOut.SendAfterHp(this, hp);
            }
        }

        public short Atk {
            get { return atk; }
            internal set {
                atk = value;
                GameIO.GameOut.SendAfterAtk(this, atk);
            }
        }

        public Attribute Attribute {
            get { return attribute; }
        }

        public bool IsDead {
            get { return hp <= 0; }
        }

        public bool IsEnabledToBeAttacked {
            get { return isEnableToBeAttacked; }
            internal set {
                if (!(this is Player)) {
                    isEnableToBeAttacked = value;
                    GameIO.GameOut.SendAfterIsEnabledToBeAttacked(this, isEnableToBeAttacked);
                }
            }
        }

        internal virtual short NumberOfAttack {
            get { return 1; }
        }

        protected Minion(string name, short cost, short hp, short atk, Attribute attribute) : base(name, cost) {
            this.hp = hp;
            this.atk = atk;
            this.isEnableToBeAttacked = true;
            this.attribute = attribute;
        }

        public virtual Task GoOnField() {
            GameIO.GameOut.SendGoOnFieldSignal(this);
            return Task.CompletedTask;
        }

        public virtual void RemoveFromField() {
            GameIO.GameOut.SendRemoveFromFieldSignal(this);
        }

        public virtual void Die() {
            if (this is Player) {
                GameIO.GameOut.SendPlayerDieSignal(this as Player);
            }
            else {
                GameIO.GameOut.SendMinionDieSignal(this);
            }
            RemoveFromField();
        }

        public virtual void Attack(Minion minion) {
            if (minion.IsEnabledToBeAttacked) {
                GameIO.GameOut.SendBeAttackedMinion(this, minion);
                minion.BeAttackedBy(this);
                BeAttackedBy(minion);
            }
            else {
                System.Diagnostics.Debug.Assert(false);
            }
        }

        public virtual void BeAttackedBy(Minion minion) {
            if (minion is Player) return;
            if (IsEnabledToBeAttacked) {
                Hp -= minion.atk;
            }
            else {
                System.Diagnostics.Debug.Assert(false);
            }
        }

        public virtual bool BeAttackedInAddition(short additionalAttack) {
            if (IsEnabledToBeAttacked) {
                Hp -= additionalAttack;
                return true;
            }
            return false;
        }

        public void IncreseAdditionalAtk(short additionalAtk) {
            atk += additionalAtk;
        }
    }
}
