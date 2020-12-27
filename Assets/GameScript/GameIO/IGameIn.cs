using System.Threading.Tasks;

namespace FinalProject
{
    interface IGameIn
    {
        // Game.cs

        [System.Obsolete("Deprecated", true)]
        Task<string> ReceiveNameOfPlayerAsync(Game receiver);

        // Player.cs

        /// <summary>
        /// You may need to call enemy.MinionsOnField to implement the method.
        /// </summary>

        Task<Minion> ReceiveEnemyMinionOnFieldAsync(Player receiver, Player enemy);

        /// <summary>
        /// You may need to call enemy.MinionsOnField to implement the method.
        /// Head (i.e. receiver) is allowed to be returned.
        /// </summary>

        Task<Minion> ReceiveEnemyHeadOrMinionOnFieldAsync(Player receiver, Player enemy);

        /// <summary>
        /// You may need to call enemy.MinionsOnField and other variables
        /// to implement the method.
        /// Return null means to quit the round.
        /// </summary>

        Task<Card> ReceiveMyCardAsync(Player receiver, Player enemy);
    }
}