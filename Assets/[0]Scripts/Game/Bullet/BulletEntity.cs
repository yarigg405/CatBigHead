using Game.Components;
using Game.Entities;
using UnityEngine;


namespace Game
{
    internal sealed class BulletEntity : TickableEntity
    {
        [SerializeField] private MoveComponent bulletMover;

        internal void Construct()
        {
            Add(bulletMover);
            var destroy = new DestroyComponent();
            Add(destroy);
        }

        private void OnDestroy()
        {
            Get<DestroyComponent>().Clear();
        }
    }
}
