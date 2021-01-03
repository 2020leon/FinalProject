using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
	class CardStatusUpdate : MonoBehaviour
	{
		private short oldAtk = -1;
		private short oldHp = -1;

		[SerializeField]
		public AnimationManager manager;

        // Update is called once per frame
        void Update()
		{
			Minion minion;
			try
			{
				minion = (Minion)GetComponent<CardDataHolder>().card;
				if (oldAtk == -1)
                {
					oldAtk = minion.Atk;
                }
				if (oldHp == -1)
                {
					oldHp = minion.Hp;
                }
			} catch (InvalidCastException)
            {
				//not in field
				return;
            }

			if (minion.Player.IsUser())
			{
				transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Cost.ToString());
				transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Atk.ToString());
				transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Hp.ToString());

				if (minion.Atk != oldAtk)
				{
					int diff = minion.Atk - oldAtk;
					transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("加攻擊/atkplus" + diff);
					manager.EnqueueAnimator(transform.GetChild(2).GetComponent<Animator>());
					oldAtk = minion.Atk;
				}
				if (minion.Hp != oldHp)
				{
					if (minion.Hp > oldHp)
					{
						transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("加血/plus1");
						manager.EnqueueAnimator(transform.GetChild(2).GetComponent<Animator>());
						oldHp = minion.Hp;
					}
					else
					{
						int diff = oldHp - minion.Hp;
						transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("扣血/minus" + diff);
						manager.EnqueueAnimator(transform.GetChild(2).GetComponent<Animator>());
						oldHp = minion.Hp;
					}
				}

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
	}
}