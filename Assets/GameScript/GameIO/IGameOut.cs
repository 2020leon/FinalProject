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
        NotMyCard
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
        void SendAfterCash(Player sender, short cash);
        void SendAfterIsInflation(Player sender, short inflationTime);
        void SendAfterStatus(Player sender, PlayerStatus playerStatus);
        void SendAfterWalletSize(Player sender, short walletSize);
        void SendStartingPlayerAndRound(Player sender, int gameRound);
        void SendPlayerCanDoNothingSignal(Player sender);
        void SendHandOfPlayerIsFullSignal(Player sender);
        void SendDeckIsEmptySignal(Player sender);
        void SendChoiceResponse(Player sender, Card chosenCard, ChoiceResponse response);
        void SendEndOfRoundSignal(Player sender);

        // Minion.cs

        void SendAfterHp(Minion sender, short hp);
        void SendAfterAtk(Minion sender, short atk);
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