using Game.Components;
using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Enemies
{
    internal sealed class EnemyEntity : TickableEntity
    {
        [SerializeField] private HealthComponent health;

        internal void Construct(TickableProcessor tickableProcessor, PlayerProvider playerProvider)
        {
            var destroy = new DestroyComponent();
            Add(destroy);
            health.OnDeath += destroy.Destroy;

            var needTickables = GetComponentsInChildren<INeedTickableProcessor>();
            foreach (var needTickable in needTickables)
            {
                needTickable.SetTickableProcessor(tickableProcessor);
            }

            var needPlayerArr = GetComponentsInChildren<INeedPlayerReference>();
            foreach (var needPlayer in needPlayerArr)
            {
                needPlayer.SetPlayer(playerProvider.Player);
            }
        }

        private void OnDestroy()
        {
            health.OnDeath -= Get<DestroyComponent>().Destroy;
        }
    }
}
