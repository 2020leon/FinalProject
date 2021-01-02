using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
	public class MinionStatusUpdate : MonoBehaviour
	{
		private short oldAtk = -1;
		private short oldHp = -1;

		[SerializeField]
		public AnimationManager manager;

        void Start()
        {
			Minion minion = GetComponent<MinionDataHolder>().minion;
			oldAtk = minion.Atk;
			oldHp = minion.Hp;
		}

        // Update is called once per frame
        void Update()
		{
			Minion minion = GetComponent<MinionDataHolder>().minion;
			transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Cost.ToString());
			transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Atk.ToString());
			transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Hp.ToString());
			if (minion.Atk != oldAtk)
            {
				int diff = minion.Atk - oldAtk;
				transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("加攻擊/atkplus" + diff);
				transform.GetChild(2).GetComponent<Animator>().SetTrigger("start");
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
					transform.GetChild(2).GetComponent<Animator>().SetTrigger("start");
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