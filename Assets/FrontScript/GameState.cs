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
        public static object InputObject = null;
        public static bool GiveUpInput = false;

        public static Task<Card> GetInput()
        {
            UserInputNeeded = true;

            return Task.Run(() =>
            {
                while (InputObject == null && !GiveUpInput)
                {
                    ;
                }
                if (InputObject == null)
                {
                    return null;
                }
                Card target = (Card)InputObject;
                UserInputNeeded = false;
                InputObject = null;
                return target;
            });
        }

        public static string UserName = ""; //This will be used as an identifier for user
        public static bool UserControllable = false;
    }

    
}