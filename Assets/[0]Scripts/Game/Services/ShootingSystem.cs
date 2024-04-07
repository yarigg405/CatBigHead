using Game.Components;
using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static UnityEngine.EventSystems.EventTrigger;


namespace Game
{
    internal sealed class ShootingSystem : MonoBehaviour
    {
        [SerializeField] private BulletsPool pool;

        [Inject] private readonly TickableProcessor _tickableProcessor;
        [Inject] private readonly IObjectResolver _resolver;

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
            bullet.SetupEntity();
            _tickableProcessor.AddTickable(bullet);
            var destroyComponent = new DestroyComponent();

            if (bullet.TryGetEntityComponent<EffectsSpawner>(out var effectSpawner))
            {
                destroyComponent.OnDestroy += effectSpawner.SpawnEffect;
            }

            destroyComponent.OnDestroy += () => pool.DespawnObject(bullet);
            bullet.AddEntityComponent(destroyComponent);

            _resolver.InjectGameObject(bullet.gameObject);
        }
    }
}
