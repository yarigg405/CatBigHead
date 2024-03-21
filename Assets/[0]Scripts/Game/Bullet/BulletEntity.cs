using Game.Components;
using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;


namespace Game
{
    internal sealed class BulletEntity : TickableEntity
    {
        [SerializeField] private DamageDealComponent damageDealer;


        [Inject]
        private void Construct(TickableProcessor tickableProcessor)
        {
            var destroy = new DestroyComponent();
            Add(destroy);
            damageDealer.OnDamageDealed += destroy.Destroy;
        }

        private void OnDestroy()
        {
            Get<DestroyComponent>().Clear();
            damageDealer.OnDamageDealed -= Get<DestroyComponent>().Destroy;
        }
    }
}
