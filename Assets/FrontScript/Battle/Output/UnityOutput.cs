using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FinalProject;

namespace FinalProject
{
    class UnityOutput : MonoBehaviour, IGameOut
    {
        [SerializeField]
        private AnimationManager animationManager;
        [SerializeField]
        private Animator roundChangeAnimator;
        [SerializeField]
        private Animator winAnimator;
        [SerializeField]
        private Animator loseAnimator;
        [SerializeField]
        private Animator tieAnimator;
        [SerializeField]
        private Animator endAnimator;
        [SerializeField]
        private Image roundImage;
        [SerializeField]
        private EnemyHeadHolder enemyHeadHolder;
        [SerializeField]
        private PlayerHeadHolder playerHeadHolder;
        [SerializeField]
        private Animator atkAnimator;
        [SerializeField]
        private Animator spellAnimator;


        [HideInInspector]
        public Queue<KeyValuePair<Player, Card>> DrawnCards = new Queue<KeyValuePair<Player, Card>>();
        [HideInInspector]
        public Queue<Card> RemoveFromHandCards = new Queue<Card>();
        [HideInInspector]
        public Queue<KeyValuePair<Player, Minion>> MinionsOnField = new Queue<KeyValuePair<Player, Minion>>();
        [HideInInspector]
        public Queue<Minion> RemoveFromFieldMinions = new Queue<Minion>();

        [SerializeField]
        private Presenter presenter;

        [HideInInspector]
        public string RoundNumber = string.Empty;
        [HideInInspector]
        public short UserCash = 0;
        [HideInInspector]
        public short EnemyCash = 0;
        [HideInInspector]
        public bool PlayerInflation = false;
        [HideInInspector]
        public bool EnemyInflation = false;

        [SerializeField]
        private GameObject EndImage;

        private Game game;

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
                    break;
                case GameStatus.ComputersTurn:
                    Debug.Log("Computers turn");
                    break;
                case GameStatus.PlayerWin:
                    Debug.Log("Player win");
                    EndImage.SetActive(true);
                    animationManager.EnqueueAnimator(winAnimator);
                    animationManager.EnqueueAnimator(endAnimator);
                    break;
                case GameStatus.ComputerWin:
                    Debug.Log("Computer win");
                    EndImage.SetActive(true);
                    animationManager.EnqueueAnimator(loseAnimator);
                    animationManager.EnqueueAnimator(endAnimator);
                    break;
                case GameStatus.Tie:
                    Debug.Log("tie");
                    EndImage.SetActive(true);
                    animationManager.EnqueueAnimator(tieAnimator);
                    animationManager.EnqueueAnimator(endAnimator);
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
            if (round < 100)
            {
                string path = "回合/round" + round.ToString();
                Debug.Log("round number path: " + path);
                roundImage.sprite = Resources.Load<Sprite>(path);
            }
            else
            {
                roundImage.sprite = Resources.Load<Sprite>("回合/round--");
            }


            animationManager.EnqueueAnimator(roundChangeAnimator);
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
            if (!sender.IsUser())
            {
                enemyHeadHolder.Enemy = sender;
                playerHeadHolder.Player = game.GetMyEnemy(sender);
            }
            else
            {
                enemyHeadHolder.Enemy = game.GetMyEnemy(sender);
                playerHeadHolder.Player = sender;
            }
        }

        public void SendAfterStatus(Player sender, PlayerStatus playerStatus)
        {
            Debug.Log("After status" + playerStatus.ToString());
        }

        public void SendChoiceResponse(Player sender, Card chosenCard, ChoiceResponse response)
        {
            Debug.Log("Choice Response: " + response.ToString());
        }

        public void SendGoOnFieldSignal(Minion sender)
        {
            RemoveFromHandCards.Enqueue(sender);
            MinionsOnField.Enqueue(new KeyValuePair<Player, Minion>(sender.Player, sender));
        }

        public void SendLaunchSpellSignal(Spell sender)
        {
            RemoveFromHandCards.Enqueue(sender);
            //test
            spellAnimator.name = sender.Name;
            spellAnimator.runtimeAnimatorController = Resources.Load("Animation/" + sender.Name) as RuntimeAnimatorController;
            animationManager.EnqueueAnimator(spellAnimator);
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
            if (inflationTime != 0)
            {
                if (sender.IsUser())
                {
                    PlayerInflation = true;
                }
                else
                {
                    EnemyInflation = true;
                }
            }
            else
            {
                if (sender.IsUser())
                {
                    PlayerInflation = false;
                }
                else
                {
                    EnemyInflation = false;
                }
            }
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
        }

        public void SendAfterAtk(Minion sender, short atk)
        {
        }

        public void SendAfterIsEnabledToBeAttacked(Minion sender, bool isEnabledToBeAttacked)
        {
            //TODO: not implemented
        }

        public void SendBeAttackedMinion(Minion sender, Minion beAttackedMinion)
        {
            //animationManager.EnqueueAnimator(atkAnimator);
            //test
            animationManager.EnqueueAnimator(atkAnimator, (animator) =>
            {
                animator.runtimeAnimatorController = Resources.Load("Animation/" + sender.Name + "L") as RuntimeAnimatorController;
            });
        }

        public void SendRemoveFromFieldSignal(Minion sender)
        {
            RemoveFromFieldMinions.Enqueue(sender);

            GameObject needToRemove = presenter.minionsOnField.Find((gameObject) =>
            {
                if (gameObject.transform.IsChildOf(presenter.selfField))
                {
                    if (gameObject.GetComponent<CardDataHolder>().card == sender)
                    {
                        animationManager.EnqueueAnimator(gameObject.transform.GetChild(0).GetComponent<Animator>(), (animator) => { }, () =>
                        {
                            Destroy(gameObject);
                        });
                        return true;
                    }
                }
                else
                {
                    if (gameObject.GetComponent<MinionDataHolder>().minion == sender)
                    {
                        animationManager.EnqueueAnimator(gameObject.transform.GetChild(0).GetComponent<Animator>(), (animator) => { }, () =>
                        {
                            Destroy(gameObject);
                        });
                        return true;
                    }
                }
                return false;
            });
        }

        public void SendMinionDieSignal(Minion sender)
        {
            //TODO: not implemented
        }

        public void SendPlayerDieSignal(Player sender)
        {

        }

        public void SendRemainingMasks(ShiZhong sender, short remainingMasks)
        {

        }

        public void SendChangingFieldSize(Player sender, short beforeFieldSize, short afterFieldSize)
        {

        }

        public void SendChangingCash(Player sender, short beforeCash, short afterCash)
        {

        }

        public void SendChangingInflationTime(Player sender, short beforeInflationTime, short afterInflationTime)
        {

        }

        public void SendChangingWalletSize(Player sender, short beforeWalletSize, short afterWalletSize)
        {

        }


        public void SendChangingHp(Minion sender, short beforeHp, short afterHp)
        {
            foreach (GameObject gameObject in presenter.minionsOnField)
            {
                if (gameObject.transform.IsChildOf(presenter.selfField))
                {
                    if (sender == gameObject.GetComponent<CardDataHolder>().card)
                    {
                        EnqueueHPAnimation(gameObject, beforeHp, afterHp, () =>
                        {
                            gameObject.GetComponent<CardStatusUpdate>().intermediateHp = afterHp;
                            gameObject.GetComponent<CardStatusUpdate>().needsUpdate = true;
                        });
                    }
                }
                else
                {
                    if (sender == gameObject.GetComponent<MinionDataHolder>().minion)
                    {
                        EnqueueHPAnimation(gameObject, beforeHp, afterHp, () =>
                        {
                            gameObject.GetComponent<MinionStatusUpdate>().intermediateHp = afterHp;
                            gameObject.GetComponent<MinionStatusUpdate>().needsUpdate = true;
                        });
                    }
                }
            }
        }

        private void EnqueueHPAnimation(GameObject gameObject, short beforeHp, short afterHp, Action end)
        {
            if (afterHp > beforeHp)
            {
                animationManager.EnqueueAnimator(gameObject.transform.GetChild(2).GetComponent<Animator>(), (animator) =>
                {
                    gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("加血/plus1");
                }, () => { end(); });
            }
            else
            {
                animationManager.EnqueueAnimator(gameObject.transform.GetChild(2).GetComponent<Animator>(), (animator) =>
                {
                    int diff = beforeHp - afterHp;
                    gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("扣血/minus" + diff);
                }, () => { end(); });
            }
        }

        public void SendChangingAtk(Minion sender, short beforeAtk, short afterAtk)
        {
            int diff = afterAtk - beforeAtk;

            foreach (GameObject gameObject in presenter.minionsOnField)
            {
                if (gameObject.transform.IsChildOf(presenter.selfField))
                {
                    if (sender == gameObject.GetComponent<CardDataHolder>().card)
                    {
                        EnqueueAtkAnimation(gameObject, afterAtk - beforeAtk, () =>
                        {
                            gameObject.GetComponent<CardStatusUpdate>().intermediateAtk = afterAtk;
                            gameObject.GetComponent<CardStatusUpdate>().needsUpdate = true;
                        });
                    }
                }
                else
                {
                    if (sender == gameObject.GetComponent<MinionDataHolder>().minion)
                    {
                        EnqueueAtkAnimation(gameObject, afterAtk - beforeAtk, () =>
                        {
                            gameObject.GetComponent<MinionStatusUpdate>().intermediateAtk = afterAtk;
                            gameObject.GetComponent<MinionStatusUpdate>().needsUpdate = true;
                        });
                    }
                }
            }
        }

        private void EnqueueAtkAnimation(GameObject gameObject, int diff, Action end)
        {
            animationManager.EnqueueAnimator(gameObject.transform.GetChild(2).GetComponent<Animator>(), (animator) =>
            {
                gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("加攻擊/atkplus" + diff);
            }, () => { end(); });
        }
    }
}