using Game.Components;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Game
{
    internal class BulletsSpawner : MonoBehaviour
    {
        [SerializeField] protected BulletsPool pool;
        [SerializeField] private ShootTimerComponent timerComponent;

        private TickableProcessor _tickableProcessor;
        private IObjectResolver _objectResolver;


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
            _objectResolver.InjectGameObject(bullet.gameObject);
            _tickableProcessor.AddTickable(bullet);
            bullet.Get<DestroyComponent>().OnDestroy += () => pool.DespawnObject(bullet);
        }

        [Inject]
        private void Construct(TickableProcessor processor, IObjectResolver resolver)
        {
            pool.OnNewObjectInstantiated += SetupBullet;
            _tickableProcessor = processor;
            _objectResolver = resolver;

            foreach (var bullet in pool.GetPooledObjects())
            {
                SetupBullet(bullet);
            }
        }
    }
}
