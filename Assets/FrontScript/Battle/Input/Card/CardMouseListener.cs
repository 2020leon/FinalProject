﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
    public class CardMouseListener : MonoBehaviour {

        public bool isOnField = false;
        private short delta;

        private void Start() {
            delta = 0;
        }

        private void Update() {
            if (!isOnField && delta != 0) {
                gameObject.transform.position += Vector3.up * .0625f * (delta > 0 ? 1 : -1);
                delta += (short)(delta > 0 ? -1 : 1);
            }
        }

        private void OnMouseDown()
        {
            if (GameState.UserInputNeeded)
            {
                GameState.CardInput = gameObject.GetComponent<CardDataHolder>().card;
            }
        }

        private void OnMouseEnter()
        {
            if (!isOnField && delta <= 0)
            {
                delta += 4;
            }
        }

        private void OnMouseExit()
        {
            if (!isOnField && delta >= 0)
            {
                delta += -4;
            }
        }
    }
}