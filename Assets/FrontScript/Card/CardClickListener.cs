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

        private void OnMouseEnter()
        {
            gameObject.transform.position += Vector3.up * 0.3f;
        }

        private void OnMouseExit()
        {
            gameObject.transform.position += Vector3.down * 0.3f;
        }
    }
}