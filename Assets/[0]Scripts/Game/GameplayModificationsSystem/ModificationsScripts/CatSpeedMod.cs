using Game.Components;
using UnityEngine;
using VContainer;


namespace Game
{
    [CreateAssetMenu(fileName = "CatSpeedMod", menuName = "Modifications/CatSpeedMod", order = 51)]
    internal sealed class CatSpeedMod : GameplayModification
    {
        [SerializeField] private float speedModifier;

        [Inject]
        private void Construct(ModificationsBlackboard modificationsBlackboard, PlayerProvider playerProvider)
        {
            var key = BlackboardConstants.MovementSpeed_Player;
            var speedMod = modificationsBlackboard.GetVariable<float>(key);
            speedMod += speedModifier;
            modificationsBlackboard.SetVariable(key, speedMod);

            var moving = playerProvider.Player.GetEntityComponent<MoveComponent>();            
            moving.MoveSpeedMoficator = speedMod;
        }
    }
}
