using Game.Components;
using Game.Entities;
using UnityEngine;
using VContainer;


namespace Game
{
    internal sealed class BulletEntity : TickableEntity
    {
        [SerializeField] private DamageDealComponent damageDealer;


        [Inject]
        private void Construct()
        {
            var destroy = new DestroyComponent();
            AddEntityComponent(destroy);
            damageDealer.OnDamageDealt += destroy.Destroy;
        }

        private void OnDestroy()
        {
            GetEntityComponent<DestroyComponent>().Clear();
            damageDealer.OnDamageDealt -= GetEntityComponent<DestroyComponent>().Destroy;
        }
    }
}