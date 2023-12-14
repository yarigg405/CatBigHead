using Game.Components;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game
{
    internal sealed class BulletsSpawner : MonoBehaviour
    {
        [SerializeField] private BulletsPool pool;
        [SerializeField] private ShootTimerComponent timerComponent;
        private TickableProcessor _tickableProcessor;


        internal void Construct(TickableProcessor tickableProcessor)
        {
            pool.OnNewObjectInstantiated += SetupBullet;
            foreach (var bullet in pool.GetPooledObjects())
            {
                SetupBullet(bullet);
            }
            _tickableProcessor = tickableProcessor;
        }


        private void OnDestroy()
        {
            pool.OnNewObjectInstantiated -= SetupBullet;

        }



        private void OnEnable()
        {
            timerComponent.OnShoot += TryShot;
        }

        private void OnDisable()
        {
            timerComponent.OnShoot -= TryShot;
        }

        private void TryShot()
        {
            var bullet = pool.SpawnObject();

            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.Get<MoveComponent>().Move(bullet.transform.forward);
        }

        private void SetupBullet(BulletEntity bullet)
        {
            bullet.Construct();
            _tickableProcessor.AddTickable(bullet);
            bullet.Get<DestroyComponent>().OnDestroy += () => pool.DespawnObject(bullet);
        }
    }
}
