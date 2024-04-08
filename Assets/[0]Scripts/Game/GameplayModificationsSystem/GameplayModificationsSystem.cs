using UnityEngine;
using VContainer;


namespace Game
{
    internal sealed class GameplayModificationsSystem : MonoBehaviour
    {
        [SerializeField] private GameplayModification[] modifications;
        [Inject] private readonly IObjectResolver _resolver;
        [Inject] private readonly ModificationsBlackboard _modificationsBlackboard;


        internal void SetupSystem()
        {
            _modificationsBlackboard.SetVariable(BlackboardConstants.BulletSpeedMod_Player, 1f);
            _modificationsBlackboard.SetVariable(BlackboardConstants.BulletSpeedMod_Enemy, 1f);
            _modificationsBlackboard.SetVariable(BlackboardConstants.RateOfFireMod_Player, 1f);
            _modificationsBlackboard.SetVariable(BlackboardConstants.RateOfFireMod_Enemy, 1f);
            _modificationsBlackboard.SetVariable(BlackboardConstants.MovementSpeed_Player, 1f);


            for (int i = 0; i < modifications.Length; i++)
            {
                var mod = modifications[i];
                _resolver.Inject(mod);
            }
        }
    }
}
