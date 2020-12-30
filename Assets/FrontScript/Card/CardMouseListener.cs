using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
    public class CardMouseListener : MonoBehaviour {

        public bool isOnField = false;

        private void OnMouseDown()
        {
            if (GameState.UserInputNeeded)
            {
                GameState.CardInput = gameObject.GetComponent<CardDataHolder>().card;
            }
        }

        private void OnMouseEnter()
        {
            if (!isOnField)
            {
                gameObject.transform.position += Vector3.up * 0.3f;
            }
        }

        private void OnMouseExit()
        {
            if (!isOnField)
            {
                gameObject.transform.position += Vector3.down * 0.3f;
            }
        }
    }
}