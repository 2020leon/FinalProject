using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinalProject;
using UnityEngine;

namespace FinalProject
{
	public class GameStarter : MonoBehaviour
	{

		[SerializeField]
		private UnityInput input;
		[SerializeField]
		private UnityOutput output;

		// Use this for initialization
		async void Start()
		{
			GameIO.GameIn = input;
			GameIO.GameOut = output;
			Game game = new Game();
			game.Start();
			await game.DoRounds();
		}
	}
}