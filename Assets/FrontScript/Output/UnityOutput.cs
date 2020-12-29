using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FinalProject
{
    class UnityOutput : MonoBehaviour, IGameOut
    {
        [SerializeField]
        private Animator roundChangeAnimator;
        [SerializeField]
        private AnimationManager animationmanager;

        private Game game;

        [HideInInspector]
        public Queue<KeyValuePair<Player, Card>> DrawnCards = new Queue<KeyValuePair<Player, Card>>();
        [HideInInspector]
        public Queue<Card> RemoveFromHandCards = new Queue<Card>();
        [HideInInspector]
        public Queue<KeyValuePair<Player, Minion>> MinionsOnField = new Queue<KeyValuePair<Player, Minion>>(); 

        [HideInInspector]
        public string RoundNumber = string.Empty;
        [HideInInspector]
        public short UserCash = 0;
        [HideInInspector]
        public short EnemyCash = 0;

        public void SendAfterStatus(Game sender, GameStatus gameStatus)
        {
            switch (gameStatus)
            {
                case GameStatus.Start:
                    Debug.Log("game started");
                    break;
                case GameStatus.Continue:
                    Debug.Log("game continue");
                    break;
                case GameStatus.PlayersTurn:
                    Debug.Log("Players turn");
                    GameState.UserControllable = true;
                    break;
                case GameStatus.ComputersTurn:
                    Debug.Log("Computers turn");
                    GameState.UserControllable = false;
                    break;
                case GameStatus.PlayerWin:
                    Debug.Log("Player win");
                    break;
                case GameStatus.ComputerWin:
                    Debug.Log("Computer win");
                    break;
                case GameStatus.Tie:
                    Debug.Log("tie");
                    break;
            }
        }

        public void SendGameStartSignal(Game sender)
        {
            game = sender;
        }

        public void SendDrawnCard(Player sender, Card drawnCard)
        {
            DrawnCards.Enqueue(new KeyValuePair<Player, Card>(sender, drawnCard));
        }

        public void SendRoundCount(Game sender, int round)
        {
            RoundNumber = round.ToString();

        }

        public void SendAfterCash(Player sender, short cash)
        {
            if (sender.IsUser())
            {
                UserCash = cash;
            }
            else
            {
                EnemyCash = cash;
            }
        }

        public void SendStartingPlayerAndRound(Player sender, int gameRound)
        {
            animationmanager.EnqueueAnimator(roundChangeAnimator);
        }

        public void SendAfterStatus(Player sender, PlayerStatus playerStatus)
        {
            //TODO: not implemented
        }

        public void SendChoiceResponse(Player sender, Card chosenCard, ChoiceResponse response)
        {

            //TODO: not implemented
        }

        public void SendGoOnFieldSignal(Minion sender)
        {
            RemoveFromHandCards.Enqueue(sender);
            MinionsOnField.Enqueue(new KeyValuePair<Player, Minion>(sender.Player, sender));
        }

        public void SendLaunchSpellSignal(Spell sender)
        {
            RemoveFromHandCards.Enqueue(sender);
        }

        public void SendEndOfRoundSignal(Player sender)
        {
            //TODO: not implemented
        }

        public void SendAfterFieldSize(Player sender, short fieldSize)
        {
            //TODO: not implemented
        }

        public void SendAfterIsInflation(Player sender, short inflationTime)
        {
            //TODO: not implemented
        }

        public void SendAfterWalletSize(Player sender, short walletSize)
        {
            //TODO: not implemented
        }

        public void SendPlayerCanDoNothingSignal(Player sender)
        {
            //TODO: not implemented
        }

        public void SendHandOfPlayerIsFullSignal(Player sender)
        {
            //TODO: not implemented
        }

        public void SendDeckIsEmptySignal(Player sender)
        {
            //TODO: not implemented
        }

        public void SendAfterHp(Minion sender, short hp)
        {
            //TODO: not implemented
        }

        public void SendAfterAtk(Minion sender, short atk)
        {
            //TODO: not implemented
        }

        public void SendAfterIsEnabledToBeAttacked(Minion sender, bool isEnabledToBeAttacked)
        {
            //TODO: not implemented
        }

        public void SendBeAttackedMinion(Minion sender, Minion beAttackedMinion)
        {
            //TODO: not implemented
        }

        public void SendRemoveFromFieldSignal(Minion sender)
        {
            //TODO: not implemented
        }

        public void SendMinionDieSignal(Minion sender)
        {
            //TODO: not implemented
        }

        public void SendPlayerDieSignal(Player sender)
        {
            //TODO: not implemented
        }

        public void SendRemainingMasks(ShiZhong sender, short remainingMasks)
        {
            //TODO: not implemented
        }
    }
}