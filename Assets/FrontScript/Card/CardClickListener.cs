using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
    public class CardClickListener : MonoBehaviour {

        private void OnMouseDown()
        {
            if (GameState.UserInputNeeded)
            {
                GameState.CardInput = gameObject.GetComponent<CardDataHolder>().card;
            }
        }
    }
}