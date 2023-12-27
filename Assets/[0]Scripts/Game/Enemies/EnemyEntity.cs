using Game.Components;
using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Enemies
{
    internal sealed class EnemyEntity : TickableEntity
    {
        [SerializeField] private HealthComponent health;

        internal void Construct(TickableProcessor tickableProcessor)
        {
            var destroy = new DestroyComponent();
            Add(destroy);
            health.OnDeath += destroy.Destroy;

            var needTickables = GetComponentsInChildren<INeedTickableProcessor>();
            foreach (var needTickable in needTickables)
            {
                needTickable.SetTickableProcessor(tickableProcessor);
            }
        }

        private void OnDestroy()
        {
            health.OnDeath -= Get<DestroyComponent>().Destroy;
        }
    }
}
