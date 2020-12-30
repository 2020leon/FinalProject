using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
    public class MinionClickListener : MonoBehaviour
    {
        private void OnMouseDown()
        {
            if (GameState.UserInputNeeded)
            {
                GameState.HeadOrMinion = gameObject.GetComponent<MinionDataHolder>().minion;
            }
        }
    }
}