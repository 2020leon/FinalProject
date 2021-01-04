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
		public Transform selfField;
		public Transform enemyField;
		[SerializeField]
		private MaterialMap minionMaterials;

		[SerializeField]
		private EnemyHeadHolder enemyHeadHolder;
		[SerializeField]
		private PlayerHeadHolder playerHeadHolder;
		[SerializeField]
		private SpriteRenderer enemyHPImage;
		[SerializeField]
		private SpriteRenderer playerHPImage;

		[SerializeField]
		private Transform endTurnButton;

		[SerializeField]
		private GameObject playerInflation;
		[SerializeField]
		private GameObject enemyInflation;

		[HideInInspector]
		public List<GameObject> cardsObjectInHand = new List<GameObject>();
		public List<GameObject> minionsOnField = new List<GameObject>();

		// Update is called once per frame
		void Update()
		{
			UpdateCash();
			AddDrawnCard();
			RemoveHandCard();
            UpdateFieldCard();
            RemoveFieldMinion();
			UpdateStatus();
			SetLight();
			UpdateInflation();
		}

		private void UpdateCash()
		{
			userCashNumber.sprite = Resources.Load<Sprite>("數字/" + model.UserCash.ToString());
			enemyCashNumber.sprite = Resources.Load<Sprite>("數字/" + model.EnemyCash.ToString());
			for (int i = 0; i < 8; i++)
			{
				if (i + 1 <= model.UserCash)
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
					cardObject.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + card.Cost.ToString());
					if (card is Minion)
					{
						cardObject.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + ((Minion)card).Atk.ToString());
						cardObject.transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + ((Minion)card).Hp.ToString());
					}
					cardObject.transform.GetChild(1).GetChild(3).gameObject.SetActive(true);
				}

				cardObject.transform.SetParent(player.IsUser() ? handsArea : enemyHandsArea);

				if (!player.IsUser())
				{
					cardObject.GetComponent<Renderer>().material = materials["卡背"];
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
					cardObject.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + card.Cost.ToString());
					cardObject.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + ((Minion)card).Atk.ToString());
					cardObject.transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + ((Minion)card).Hp.ToString());
					cardObject.transform.SetParent(selfField);

					if (minion is ShiZhong)
                    {
						cardObject.transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
                    }

					minionsOnField.Add(cardObject);
				}
				else
				{
					GameObject minionObject = Instantiate(fieldMinionPrefab);
					minionObject.GetComponent<Renderer>().material = minionMaterials[minion.Name];
					minionObject.GetComponent<MinionDataHolder>().minion = minion;
					minionObject.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Cost.ToString());
					minionObject.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Atk.ToString());
					minionObject.transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("數字/" + minion.Hp.ToString());
					minionObject.transform.SetParent(enemyField);

					if (minion is ShiZhong)
					{
						minionObject.transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
					}

					minionsOnField.Add(minionObject);
				}
			}
		}

		private void UpdateStatus()
		{
			UpdatePlayerHP();
			UpdateEnemyHP();
		}

		private void SetLight() {
			foreach (var cardObject in cardsObjectInHand) {
				var cardDataHolder = cardObject.GetComponent<CardDataHolder>();
				var card = cardDataHolder != null ? cardDataHolder.card : null;
				var player = card != null ? card.Player : null;
				if (player != null && player.IsUser()) {
					var light = cardObject.transform.GetChild(1).GetChild(3);
					light.gameObject.SetActive(
						card.Cost + Inflation.ExtraCost * player.InflationTime <= player.Cash && GameState.InputState == InputState.GetCardInput && (card is Spell || !player.IsFieldFull)
					);
					var color = light.GetComponent<SpriteRenderer>().color;
					color.a = (Mathf.Sin(2.5f * Time.time) + 3) / 4f;
					light.GetComponent<SpriteRenderer>().color = color;
				}
			}
			foreach (var cardObject in minionsOnField) {
				var cardDataHolder = cardObject.GetComponent<CardDataHolder>();
				var minionDataHolder = cardObject.GetComponent<MinionDataHolder>();
				var minion = cardDataHolder != null ? cardDataHolder.card as Minion : minionDataHolder.minion;
				var player = minion != null ? minion.Player : null;
				if (player != null) {
					var light = cardObject.transform.GetChild(1).GetChild(3);
					if (player.IsUser()) {
						light.gameObject.SetActive(
							player.Status == PlayerStatus.Acting && !player.AttackedMinions.Contains(minion) && GameState.InputState == InputState.GetCardInput && (player.Game.GetMyEnemy(player).CanAnyMinionAttack || player.Game.GetMyEnemy(player).MinionsOnField.Count == 0)
						);
					}
					else {
						light.gameObject.SetActive(
							(GameState.InputState == InputState.GetEnemyHeadOrMinion && minion.IsEnabledToBeAttacked) || (GameState.InputState == InputState.GetEnemyMinion)
						);
					}
					var color = light.GetComponent<SpriteRenderer>().color;
					color.a = (Mathf.Sin(2.5f * Time.time) + 3) / 4f;
					light.GetComponent<SpriteRenderer>().color = color;
				}
			}
			if (GameState.InputState == InputState.GetCardInput) {
				var image = endTurnButton.GetComponent<Image>();
				if (image.sprite.name != "EndTurnGreenButton") {
					image.sprite = Resources.Load<Sprite>($"素材/EndTurnGreenButton");
				}
				else {
					var color = image.color;
					color.a = (Mathf.Sin(2.5f * Time.time) + 1) / 2f;
					image.color = color;
				}
			}
			else {
				endTurnButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("素材/EndTurnButton");
			}
			var r = 110f;
			var g = 256f;
			var b = 0f;
			if (GameState.InputState == InputState.GetEnemyHeadOrMinion && (enemyHeadHolder.Enemy as Player).MinionsOnField.Count == 0) {
				var color = enemyHPImage.color;
				color.r = Mathf.Sin(2.5f * Time.time) / 2f * (1 - r / 256f) + 1 - (1 - r / 256f) / 2f;
				color.g = Mathf.Sin(2.5f * Time.time) / 2f * (1 - g / 256f) + 1 - (1 - g / 256f) / 2f;
				color.b = Mathf.Sin(2.5f * Time.time) / 2f * (1 - b / 256f) + 1 - (1 - b / 256f) / 2f;
				enemyHPImage.color = color;
			}
			else {
				var color = enemyHPImage.color;
				color.r = color.g = color.b = 1;
				enemyHPImage.color = color;
			}
		}

		private void UpdatePlayerHP()
		{
			if (playerHeadHolder.Player != null)
			{
				playerHPImage.sprite = Resources.Load<Sprite>("數字/" + playerHeadHolder.Player.Hp.ToString());
			}
		}

		private void UpdateEnemyHP()
		{
			if (enemyHeadHolder.Enemy != null)
			{
				enemyHPImage.sprite = Resources.Load<Sprite>("數字/" + enemyHeadHolder.Enemy.Hp.ToString());
			}
		}

		private void RemoveFieldMinion()
        {
            while (model.RemoveFromFieldMinions.Count > 0)
            {
                Minion target = model.RemoveFromFieldMinions.Dequeue();
				GameObject needToRemove = minionsOnField.Find((gameObject) =>
				{
					if (gameObject.transform.IsChildOf(selfField))
					{
						if (gameObject.GetComponent<CardDataHolder>().card == target)
						{
							return true;
						}
					}
					else
					{
						if (gameObject.GetComponent<MinionDataHolder>().minion == target)
						{
							return true;
						}
					}
					return false;
				});

				minionsOnField.Remove(needToRemove);
            }
        }

		private void UpdateInflation()
        {
			enemyInflation.SetActive(model.EnemyInflation);
			playerInflation.SetActive(model.PlayerInflation);
        }
	}
}