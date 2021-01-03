namespace FinalProject
{
    class ShiZhong : Minion
    {
        private short mask;

        public short Mask {
            get { return mask; }
        }

        public ShiZhong() : base("陳時中", 3, 3, 2, Attribute.Green) {
            mask = 3;
        }

        public override void BeAttackedBy(Minion minion) {
            if (mask > 0 && !(minion is Player)) {
                mask--;
                GameIO.GameOut.SendRemainingMasks(this, mask);
            }
            else base.BeAttackedBy(minion);
        }

        public override bool BeAttackedInAddition(short additionalAttack)
        {
            if (mask <= 0) return base.BeAttackedInAddition(additionalAttack);
            return false;
        }
    }
}