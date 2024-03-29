using Game.Components;
using Game.Entities;
using UnityEngine;


namespace Game
{
    internal sealed class BulletEntity : TickableEntity
    {
        [SerializeField] private DamageDealComponent damageDealer;       

        private void Start()
        {
            var destroy = GetEntityComponent<DestroyComponent>();
            damageDealer.OnDamageDealt += destroy.Destroy;
        }

        private void OnDestroy()
        {
            GetEntityComponent<DestroyComponent>().Clear();
            damageDealer.OnDamageDealt -= GetEntityComponent<DestroyComponent>().Destroy;
        }
    }
}