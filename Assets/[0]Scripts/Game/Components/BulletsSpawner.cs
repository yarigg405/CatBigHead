using Game.Components;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game
{
    internal class BulletsSpawner : MonoBehaviour, INeedTickableProcessor
    {
        [SerializeField] protected BulletsPool pool;
        [SerializeField] private ShootTimerComponent timerComponent;
        private TickableProcessor _tickableProcessor;


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

        protected virtual void TryShot()
        {
            var bullet = pool.SpawnObject();

            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.Get<MoveComponent>().Move(bullet.transform.forward);
        }

        private void SetupBullet(BulletEntity bullet)
        {
            bullet.Construct(_tickableProcessor);
            _tickableProcessor.AddTickable(bullet);
            bullet.Get<DestroyComponent>().OnDestroy += () => pool.DespawnObject(bullet);
        }

        void INeedTickableProcessor.SetTickableProcessor(TickableProcessor processor)
        {
            pool.OnNewObjectInstantiated += SetupBullet;
            _tickableProcessor = processor;

            foreach (var bullet in pool.GetPooledObjects())
            {
                SetupBullet(bullet);
            }
        }
    }
}
