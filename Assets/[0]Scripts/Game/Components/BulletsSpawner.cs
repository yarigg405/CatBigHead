using Game.Components;
using Game.Entities;
using UnityEngine;
using VContainer;


namespace Game
{
    internal class BulletsSpawner : MonoBehaviour
    {
        [SerializeField] private ShootTimerComponent timerComponent;
        [SerializeField] private BulletEntity bulletPrefab;

        private ShootingSystem _shootingSystem;


        [Inject]
        private void Construct(ShootingSystem shootingSystem)
        {
            _shootingSystem = shootingSystem;
            _shootingSystem.PrepareBullets(bulletPrefab);
        }

        private void Start()
        {

        }

        private void OnEnable()
        {
            timerComponent.OnShoot += TryShot;
        }

        private void OnDisable()
        {
            timerComponent.OnShoot -= TryShot;
        }

        protected Entity CreateBullet()
        {
            return _shootingSystem.SpawnBullet(bulletPrefab);
        }

        protected virtual void TryShot()
        {
            var bullet = CreateBullet();

            var tr = transform;
            var btr = bullet.transform;

            btr.position = tr.position;
            btr.rotation = tr.rotation;
            bullet.GetEntityComponent<MoveComponent>().Move(btr.forward);
        }
    }
}