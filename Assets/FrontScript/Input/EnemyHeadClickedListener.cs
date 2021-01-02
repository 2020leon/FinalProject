using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
    public class EnemyHeadClickedListener : MonoBehaviour
    {

        private void OnMouseDown()
        {
            if (GameState.UserInputNeeded)
            {
                GameState.HeadOrMinion = GetComponent<EnemyHeadHolder>().enemy;
            }
        }
    }
}