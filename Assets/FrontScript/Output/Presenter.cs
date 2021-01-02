using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FinalProject
{
	public class Presenter : MonoBehaviour
	{

		[SerializeField]
		private UnityOutput model;

		[SerializeField]
		private GameObject cardPrefab;
		[SerializeField]
		private Transform handsArea;
		[SerializeField]
		private Transform enemyHandsArea;
		[SerializeField]
		private MaterialMap materials;

		[SerializeField]
		private GameObject cashPrefab;
		[SerializeField]
		private GameObject emptyCashPrefab;
		[SerializeField]
		private Transform cashArea;
		[SerializeField]
		private Image enemyCashNumber;
		[SerializeField]
		private Image userCashNumber;

		[SerializeField]
		private GameObject fieldMinionPrefab;
		[SerializeField]
		private Transform selfField;
		[SerializeField]
		private Transform enemyField;
		[SerializeField]
		private MaterialMap minionMaterials;

		private List<GameObject> cardsObjectInHand = new List<GameObject>();

		// Update is called once per frame
		void Update()
		{
			UpdateCash();
			AddDrawnCard();
			RemoveHandCard();
			UpdateFieldCard();
		}

		private void UpdateCash()
        {
			userCashNumber.sprite = Resources.Load<Sprite>("數字/" + model.UserCash.ToString());
			enemyCashNumber.sprite = Resources.Load<Sprite>("數字/" + model.EnemyCash.ToString());
			for (int i = 0; i < 8; i++)
            {
				if (i+1 <= model.UserCash)
                {
					 cashArea.GetChild(i).GetComponent<Image>().enabled = true;
				}
				else
                {
					cashArea.GetChild(i).GetComponent<Image>().enabled = false;
				}
            }
        }

		private void AddDrawnCard()
        {
			while (model.DrawnCards.Count > 0)
			{
				KeyValuePair<Player, Card> pair = model.DrawnCards.Dequeue();
				Player player = pair.Key;
				Card card = pair.Value;

				GameObject cardObject = Instantiate(cardPrefab);
				cardObject.GetComponent<Renderer>().material = materials[card.Name];
				cardObject.GetComponent<CardDataHolder>().card = card;

				if (player.IsUser())
                {
					cardObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + card.Cost.ToString());
					if (card is Minion)
                    {
						cardObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + ((Minion)card).Atk.ToString());
						cardObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + ((Minion)card).Hp.ToString());
					}
					cardObject.transform.GetChild(3).gameObject.SetActive(true);
				}

				cardObject.transform.SetParent(player.IsUser() ? handsArea : enemyHandsArea);

				if (!player.IsUser())
				{
					cardObject.GetComponent<Renderer>().material = null;
					Destroy(cardObject.GetComponent<CardMouseListener>());
				}
				cardsObjectInHand.Add(cardObject);
			}
		}

		private void RemoveHandCard()
        {
			while (model.RemoveFromHandCards.Count > 0)
            {
				Card c = model.RemoveFromHandCards.Dequeue();
				GameObject cardObject = cardsObjectInHand.Find((gameObject) =>
				{
					return gameObject.GetComponent<CardDataHolder>().card == c;
				});
				cardsObjectInHand.Remove(cardObject);
				Destroy(cardObject);
            }
        }

		private void UpdateFieldCard()
        {
			while (model.MinionsOnField.Count > 0)
            {
				KeyValuePair<Player, Minion> pair = model.MinionsOnField.Dequeue();
				Player player = pair.Key;
				Minion minion = pair.Value;

				if (player.IsUser())
                {
					GameObject cardObject = Instantiate(cardPrefab);
					cardObject.GetComponent<Renderer>().material = materials[minion.Name];
					cardObject.GetComponent<CardDataHolder>().card = minion;
					cardObject.GetComponent<CardMouseListener>().isOnField = true;
					Card card = minion;
					cardObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + card.Cost.ToString());
					cardObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + ((Minion)card).Atk.ToString());
					cardObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + ((Minion)card).Hp.ToString());
					cardObject.transform.SetParent(selfField);
				}
				else
                {
					GameObject minionObject = Instantiate(fieldMinionPrefab);
					minionObject.GetComponent<Renderer>().material = minionMaterials[minion.Name];
					minionObject.GetComponent<MinionDataHolder>().minion = minion;
					minionObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Cost.ToString());
					minionObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Atk.ToString());
					minionObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Hp.ToString());
					minionObject.transform.SetParent(enemyField); 
				}
            }
        }
	}
}