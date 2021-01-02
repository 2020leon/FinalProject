using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
    public class EndTurnButtonClick : MonoBehaviour
    {
        public void onClick()
        {
            GameState.GiveUpInput = true;
        }
    }
}