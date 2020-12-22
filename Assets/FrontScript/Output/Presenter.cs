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
		private GameObject fieldMinionPrefab;
		[SerializeField]
		private Transform selfField;
		[SerializeField]
		private Transform enemyField;
		[SerializeField]
		private MaterialMap minionMaterials;

		[SerializeField]
		private Text roundNumberText;
		[SerializeField]
		private Text userCashText;
		[SerializeField]
		private Text enemyCashText;

		private List<GameObject> cardsObjectInHand = new List<GameObject>();

		// Update is called once per frame
		void Update()
		{
			roundNumberText.text = model.RoundNumber;
			enemyCashText.text = model.EnemyCash;
			userCashText.text = model.UserCash;
			AddDrawnCard();
			RemoveHandCard();
			UpdateFieldCard();
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
				cardObject.transform.parent = player.IsUser() ? handsArea : enemyHandsArea;

				if (!player.IsUser())
				{
					cardObject.GetComponent<Renderer>().material = null;
					Destroy(cardObject.GetComponent<CardClickListener>());
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

				GameObject minionObject = Instantiate(fieldMinionPrefab);
				minionObject.GetComponent<Renderer>().material = minionMaterials[minion.Name];
				minionObject.GetComponent<MinionDataHolder>().minion = minion;
				minionObject.transform.parent = player.IsUser() ? selfField : enemyField;

				if (!player.IsUser())
				{
					Destroy(minionObject.GetComponent<CardClickListener>());
				}
            }
        }
	}
}