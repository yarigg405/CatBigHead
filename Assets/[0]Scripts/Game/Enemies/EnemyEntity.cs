using Game.Components;
using Game.Entities;
using UnityEngine;
using VContainer;


namespace Game.Enemies
{
    internal sealed class EnemyEntity : TickableEntity
    {
        [SerializeField] private HealthComponent health;


        [Inject]
        private void Construct()
        {
            var destroy = new DestroyComponent();
            AddEntityComponent(destroy);
            health.OnDeath += destroy.Destroy;
        }

        private void OnDestroy()
        {
            health.OnDeath -= GetEntityComponent<DestroyComponent>().Destroy;
        }
    }
}