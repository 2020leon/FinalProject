namespace FinalProject
{
    enum ChoiceResponse {
        Success,
        Attacked,
        NoEnemyMinionCanBeAttacked,
        AttackAfterPuttingMinionsOnField,
        FieldIsFull,
        DisabledToBeAttacked,
        LackOfCash,
        NotMyCard,
        NotCardOnEnemyField
    }

    interface IGameOut
    {
        // Game.cs

        void SendRoundCount(Game sender, int round);
        void SendAfterStatus(Game sender, GameStatus gameStatus);
        void SendGameStartSignal(Game sender);

        // Player.cs

        void SendDrawnCard(Player sender, Card drawnCard);
        void SendAfterFieldSize(Player sender, short fieldSize);
        void SendChangingFieldSize(Player sender, short beforeFieldSize, short afterFieldSize);
        void SendAfterCash(Player sender, short cash);
        void SendChangingCash(Player sender, short beforeCash, short afterCash);
        void SendAfterIsInflation(Player sender, short inflationTime);
        void SendChangingInflationTime(Player sender, short beforeInflationTime, short afterInflationTime);
        void SendAfterStatus(Player sender, PlayerStatus playerStatus);
        void SendAfterWalletSize(Player sender, short walletSize);
        void SendChangingWalletSize(Player sender, short beforeWalletSize, short afterWalletSize);
        void SendStartingPlayerAndRound(Player sender, int gameRound);
        void SendPlayerCanDoNothingSignal(Player sender);
        void SendHandOfPlayerIsFullSignal(Player sender);
        void SendDeckIsEmptySignal(Player sender);
        void SendChoiceResponse(Player sender, Card chosenCard, ChoiceResponse response);
        void SendEndOfRoundSignal(Player sender);

        // Minion.cs

        void SendAfterHp(Minion sender, short hp);
        void SendChangingHp(Minion sender, short beforeHp, short afterHp);
        void SendAfterAtk(Minion sender, short atk);
        void SendChangingAtk(Minion sender, short beforeAtk, short afterAtk);
        void SendAfterIsEnabledToBeAttacked(Minion sender, bool isEnabledToBeAttacked);
        void SendBeAttackedMinion(Minion sender, Minion beAttackedMinion);
        void SendRemoveFromFieldSignal(Minion sender);
        void SendMinionDieSignal(Minion sender);
        void SendPlayerDieSignal(Player sender);
        void SendGoOnFieldSignal(Minion sender);

        // Spell.cs

        void SendLaunchSpellSignal(Spell sender);

        // Minion/ShiZhong.cs

        void SendRemainingMasks(ShiZhong sender, short remainingMasks);
    }
}