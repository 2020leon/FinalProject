using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
	class CardStatusUpdate : MonoBehaviour
	{

		public bool needsUpdate = true;
		public short intermediateHp = short.MinValue;
		public short intermediateAtk = short.MinValue;

		// Update is called once per frame
		void Update()
		{
			if (needsUpdate)
            {
				UpdateStatus(intermediateHp, intermediateAtk);
				intermediateHp = short.MinValue;
				intermediateAtk = short.MinValue;
				needsUpdate = false;
            }

			Minion minion;
			try
			{
				minion = (Minion)GetComponent<CardDataHolder>().card;
			}
			catch (InvalidCastException)
			{
				//not in field
				return;
			}

			if (minion.Player.IsUser())
			{
				if (minion is ShiZhong)
				{
					if (((ShiZhong)minion).Mask > 0)
					{
						transform.GetChild(1).GetChild(4).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + ((ShiZhong)minion).Mask);
					}
					else
					{
						transform.GetChild(1).GetChild(4).gameObject.SetActive(false);
					}
				}
			}
		}

		void UpdateStatus(short hp, short atk)
        {
			Minion minion;
			try
			{
				minion = (Minion)GetComponent<CardDataHolder>().card;
			}
			catch (InvalidCastException)
			{
				//not in field
				return;
			}

			if (minion.Player.IsUser())
			{
				transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Cost.ToString());
				transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + (atk == short.MinValue ? minion.Atk.ToString() : atk.ToString()));
				transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + (hp == short.MinValue ? minion.Hp.ToString() : hp.ToString()));
			}
		}
	}
}