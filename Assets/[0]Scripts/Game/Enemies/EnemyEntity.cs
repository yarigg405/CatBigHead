using Game.Components;
using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;


namespace Game.Enemies
{
    internal sealed class EnemyEntity : TickableEntity
    {
        [SerializeField] private HealthComponent health;


        [Inject]
        private void Construct(TickableProcessor tickableProcessor, PlayerProvider playerProvider)
        {
            var destroy = new DestroyComponent();
            Add(destroy);
            health.OnDeath += destroy.Destroy;
        }

        private void OnDestroy()
        {
            health.OnDeath -= Get<DestroyComponent>().Destroy;
        }
    }
}
