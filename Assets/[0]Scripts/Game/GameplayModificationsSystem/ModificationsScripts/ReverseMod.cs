using Game.Player;
using UnityEngine;
using VContainer;


namespace Game
{
    [CreateAssetMenu(fileName = "ReverseMod", menuName = "Modifications/ReverseMod", order = 51)]
    internal sealed class ReverseMod : GameplayModification
    {
        [Inject]
        private void Construct(PlayerProvider playerProvider)
        {
            playerProvider.Player.GetEntityComponent<PlayerMover>().IsInverted = true;
        }
    }
}
