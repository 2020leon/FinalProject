using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FinalProject
{
	class UnityInput : MonoBehaviour, IGameIn
    {

        public Task<Card> ReceiveMyCardAsync(Player receiver, Player enemy)
        {
            return GameState.GetCardInput();
        }

        public Task<Minion> ReceiveEnemyHeadOrMinionOnFieldAsync(Player receiver, Player enemy)
        {
            return GameState.GetEnemyHeadOrMinion();
        }

        public Task<Minion> ReceiveEnemyMinionOnFieldAsync(Player receiver, Player enemy)
        {
            //TODO: not implemented
            //this will just hang
            return Task.Run(() => { while (true) {; } return (Minion)new GuoYu(); });
        }   
    }

}