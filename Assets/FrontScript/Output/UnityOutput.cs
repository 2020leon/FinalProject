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
        private Animator gameStartAnimator;
        [SerializeField]
        private Animator playerTurnAnimator;
        [SerializeField]
        private Animator enemyTurnAnimator;

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
        public string UserCash = string.Empty;
        [HideInInspector]
        public string EnemyCash = string.Empty;

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
            gameStartAnimator.SetTrigger("start");
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
                UserCash = cash.ToString();
            }
            else
            {
                EnemyCash = cash.ToString();
            }
        }

        public void SendStartingPlayerAndRound(Player sender, int gameRound)
        {
            if (sender.IsUser())
            {
                GameState.UserControllable = true;
                playerTurnAnimator.SetTrigger("start");
            }
            else
            {
                GameState.UserControllable = false;
                enemyTurnAnimator.SetTrigger("start");
            }
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
            throw new NotImplementedException();
        }

        public void SendAfterIsInflation(Player sender, short inflationTime)
        {
            throw new NotImplementedException();
        }

        public void SendAfterWalletSize(Player sender, short walletSize)
        {
            throw new NotImplementedException();
        }

        public void SendPlayerCanDoNothingSignal(Player sender)
        {
            throw new NotImplementedException();
        }

        public void SendHandOfPlayerIsFullSignal(Player sender)
        {
            //TODO: not implemented
        }

        public void SendDeckIsEmptySignal(Player sender)
        {
            throw new NotImplementedException();
        }

        public void SendAfterHp(Minion sender, short hp)
        {
            throw new NotImplementedException();
        }

        public void SendAfterAtk(Minion sender, short atk)
        {
            throw new NotImplementedException();
        }

        public void SendAfterIsEnabledToBeAttacked(Minion sender, bool isEnabledToBeAttacked)
        {
            throw new NotImplementedException();
        }

        public void SendBeAttackedMinion(Minion sender, Minion beAttackedMinion)
        {
            throw new NotImplementedException();
        }

        public void SendRemoveFromFieldSignal(Minion sender)
        {
            throw new NotImplementedException();
        }

        public void SendMinionDieSignal(Minion sender)
        {
            throw new NotImplementedException();
        }

        public void SendPlayerDieSignal(Player sender)
        {
            throw new NotImplementedException();
        }

        public void SendRemainingMasks(ShiZhong sender, short remainingMasks)
        {
            throw new NotImplementedException();
        }
    }
}