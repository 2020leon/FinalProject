using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
    public class EndTurnButtonClick : MonoBehaviour
    {
        public void onClick()
        {
            if (GameState.InputState != InputState.Ready) {
                GameState.GiveUpInput = true;
            }
        }
    }
}