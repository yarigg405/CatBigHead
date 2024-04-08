using UnityEngine;
using VContainer;


namespace Game
{
    [CreateAssetMenu(fileName = "CatSpeedMod", menuName = "Modifications/CatSpeedMod", order = 51)]
    internal sealed class CatSpeedMod : GameplayModification
    {
        [SerializeField] private float speedModifier;

        [Inject]
        private void Construct(ModificationsBlackboard modificationsBlackboard)
        {
            var key = BlackboardConstants.MovementSpeed_Player;
            var speedMod = modificationsBlackboard.GetVariable<float>(key);
            speedMod += speedModifier;
            modificationsBlackboard.SetVariable(key, speedMod);
        }
    }
}
