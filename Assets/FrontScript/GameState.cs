using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FinalProject
{
    class GameState: MonoBehaviour
    {
        public static bool UserInputNeeded = false;

        public static Card CardInput = null;
        public static Minion HeadOrMinion = null;

        public static bool GiveUpInput = false;

        public static Task<Card> GetCardInput()
        {
            UserInputNeeded = true;

            return Task.Run(() =>
            {
                while (CardInput == null && !GiveUpInput)
                {
                    ;
                }
                if (CardInput == null)
                {
                    GiveUpInput = false;
                    return null;
                }
                Card target = CardInput;
                UserInputNeeded = false;
                CardInput = null;
                return target;
            });
        }

        public static Task<Minion> GetEnemyHeadOrMinion()
        {
            UserInputNeeded = true;
            return Task.Run(() =>
            {
                while (HeadOrMinion == null && !GiveUpInput)
                {
                    ;
                }
                if (HeadOrMinion == null)
                {
                    GiveUpInput = false;
                    return null;
                }
                Minion target = HeadOrMinion;
                UserInputNeeded = false;
                HeadOrMinion = null;
                return target;
            });
        }

        public readonly static string UserName = "Player"; //This will be used as an identifier for user
    }

    
}