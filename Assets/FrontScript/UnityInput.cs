using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FinalProject
{
	class UnityInput : MonoBehaviour, IGameIn
	{

        public Task<string> ReceiveNameOfPlayerAsync(Game receiver)
        {
            //temporary
            GameState.UserName = "Player";
            return Task.Run(() => GameState.UserName);
        }

        public Task<Card> ReceiveMyCardAsync(Player receiver, Player enemy)
        {
            return GameState.GetInput();
        }

        public Task<Minion> ReceiveEnemyHeadOrMinionOnFieldAsync(Player receiver, Player enemy)
        {
            throw new System.NotImplementedException();
        }

        public Task<Minion> ReceiveEnemyMinionOnFieldAsync(Player receiver, Player enemy)
        {
            throw new System.NotImplementedException();
        }   
    }

}