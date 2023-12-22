using Game.Components;
using Game.Entities;
using UnityEngine;


namespace Game
{
    internal sealed class BulletEntity : TickableEntity
    {
        [SerializeField] private DamageDealComponent damageDealer;

        internal void Construct()
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
