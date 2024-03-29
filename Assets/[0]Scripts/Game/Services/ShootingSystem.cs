using Game.Components;
using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;


namespace Game
{
    internal sealed class ShootingSystem : MonoBehaviour
    {
        [SerializeField] private BulletsPool pool;

        [Inject] private readonly TickableProcessor _tickableProcessor;

        private void OnEnable()
        {
            pool.OnNewObjectInstantiated += SetupBullet;
        }

        private void OnDisable()
        {
            pool.OnNewObjectInstantiated -= SetupBullet;
        }


        internal Entity SpawnBullet(BulletEntity bulletPrefab)
        {
            var bullet = pool.SpawnObject(bulletPrefab, null);
            return bullet;
        }

        internal void PrepareBullets(BulletEntity bulletPrefab)
        {
            var count = pool.GetCountOf(bulletPrefab);
            if (count == 0)
            {
                pool.PopulateWith(bulletPrefab, 10);
            }
        }

        private void SetupBullet(BulletEntity bullet)
        {
            _tickableProcessor.AddTickable(bullet);
            var destroyComponent = new DestroyComponent();
            destroyComponent.OnDestroy += () => pool.DespawnObject(bullet);
            bullet.AddEntityComponent(destroyComponent);
        }
    }
}
