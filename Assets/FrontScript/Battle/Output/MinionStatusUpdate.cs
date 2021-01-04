using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject
{
	public class MinionStatusUpdate : MonoBehaviour
	{
        // Update is called once per frame
        void Update()
		{
			Minion minion = GetComponent<MinionDataHolder>().minion;
			transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Cost.ToString());
			transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Atk.ToString());
			transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Hp.ToString());

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