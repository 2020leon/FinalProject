using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
    //This class provides UnityOutput with some additional convenient methods
    static class OutputExtensions
    {
        public static bool IsUser(this Player player)
        {
            return player.Name == GameState.UserName;
        }
    }
}