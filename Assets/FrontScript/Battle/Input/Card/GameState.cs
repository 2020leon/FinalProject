using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FinalProject
{
    enum InputState
    {
        Ready, GetCardInput, GetEnemyHeadOrMinion, GetEnemyMinion
    }

    class GameState: MonoBehaviour
    {
        public static bool UserInputNeeded = false;

        public static Card CardInput = null;
        public static Minion HeadOrMinion = null;
        public static Minion Minion = null;

        public static bool GiveUpInput = false;

        public static InputState InputState = InputState.Ready;

        public static Task<Card> GetCardInput()
        {
            InputState = InputState.GetCardInput;
            UserInputNeeded = true;

            return Task.Run(() =>
            {
                while (CardInput == null && !GiveUpInput)
                {
                    ;
                }
                InputState = InputState.Ready;
                if (CardInput == null)
                {
                    GiveUpInput = false;
                    return null;
                }
                Card target = CardInput;
                UserInputNeeded = false;
                CardInput = null;
                HeadOrMinion = null;
                Minion = null;
                return target;
            });
        }

        public static Task<Minion> GetEnemyHeadOrMinion()
        {
            InputState = InputState.GetEnemyHeadOrMinion;
            UserInputNeeded = true;
            return Task.Run(() =>
            {
                while (HeadOrMinion == null && !GiveUpInput)
                {
                    ;
                }
                InputState = InputState.Ready;
                if (HeadOrMinion == null)
                {
                    GiveUpInput = false;
                    return null;
                }
                Minion target = HeadOrMinion;
                UserInputNeeded = false;
                CardInput = null;
                HeadOrMinion = null;
                Minion = null;
                return target;
            });
        }

        public static Task<Minion> GetEnemyMinion()
        {
            InputState = InputState.GetEnemyMinion;
            UserInputNeeded = true;
            return Task.Run(() =>
            {
                while (Minion == null && !GiveUpInput)
                {
                    ;
                }
                InputState = InputState.Ready;
                if (Minion == null)
                {
                    GiveUpInput = false;
                    return null;
                }
                Minion target = Minion;
                UserInputNeeded = false;
                CardInput = null;
                HeadOrMinion = null;
                Minion = null;
                InputState = InputState.Ready;
                return target;
            });
        }

        public readonly static string UserName = "Player"; //This will be used as an identifier for user
    }

    
}