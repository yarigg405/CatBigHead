using Game.Components;
using Game.Entities;
using UnityEngine;
using VContainer;


namespace Game.Enemies
{
    internal sealed class EnemyEntity : TickableEntity
    {
        [SerializeField] private HealthComponent health;
        [Inject] private readonly ModificationsBlackboard _modificationsBlackboard;


        [Inject]
        private void Construct()
        {
            var destroy = new DestroyComponent();
            AddEntityComponent(destroy);
            health.OnDeath += destroy.Destroy;

            var shotTimer = GetEntityComponent<ShootTimerComponent>();
            var timerModificator = _modificationsBlackboard.GetVariable<float>(BlackboardConstants.RateOfFireMod_Enemy);
            shotTimer.ShotTimeModificator = timerModificator;
        }

        private void OnDestroy()
        {
            health.OnDeath -= GetEntityComponent<DestroyComponent>().Destroy;
        }
    }
}