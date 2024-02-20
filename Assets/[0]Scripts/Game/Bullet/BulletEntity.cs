using Game.Components;
using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game
{
    internal sealed class BulletEntity : TickableEntity
    {
        [SerializeField] private DamageDealComponent damageDealer;

        internal void Construct(TickableProcessor tickableProcessor)
        {
            var destroy = new DestroyComponent();
            Add(destroy);
            damageDealer.OnDamageDealed += destroy.Destroy;

            var needTickables = GetComponentsInChildren<INeedTickableProcessor>();
            foreach (var needTickable in needTickables)
            {
                needTickable.SetTickableProcessor(tickableProcessor);
            }
        }

        private void OnDestroy()
        {
            Get<DestroyComponent>().Clear();
            damageDealer.OnDamageDealed -= Get<DestroyComponent>().Destroy;
        }
    }
}
