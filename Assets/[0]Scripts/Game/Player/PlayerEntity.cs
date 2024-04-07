using Game.Components;
using Game.Entities;
using VContainer;


namespace Game.Player
{
    internal sealed class PlayerEntity : TickableEntity
    {
        [Inject] private readonly ModificationsBlackboard _modificationsBlackboard;


        [Inject]
        private void Construct()
        {
            var shotTimer = GetEntityComponent<ShootTimerComponent>();
            var timerModificator = _modificationsBlackboard.GetVariable<float>(BlackboardConstants.RateOfFireMod_Player);
            shotTimer.ShotTimeModificator = timerModificator;
        }
    }
}