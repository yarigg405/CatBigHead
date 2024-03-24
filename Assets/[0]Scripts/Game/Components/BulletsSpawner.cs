using Game.Components;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Game
{
    internal class BulletsSpawner : MonoBehaviour
    {
        private IObjectResolver _objectResolver;

        private TickableProcessor _tickableProcessor;
        [SerializeField] protected BulletsPool pool;
        [SerializeField] private ShootTimerComponent timerComponent;


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

            var tr = transform;
            var btr = bullet.transform;

            btr.position = tr.position;
            btr.rotation = tr.rotation;
            bullet.GetEntityComponent<MoveComponent>().Move(btr.forward);
        }

        private void SetupBullet(BulletEntity bullet)
        {
            _objectResolver.InjectGameObject(bullet.gameObject);
            _tickableProcessor.AddTickable(bullet);
            bullet.GetEntityComponent<DestroyComponent>().OnDestroy += () => pool.DespawnObject(bullet);
        }

        [Inject]
        private void Construct(TickableProcessor processor, IObjectResolver resolver)
        {
            pool.OnNewObjectInstantiated += SetupBullet;
            _tickableProcessor = processor;
            _objectResolver = resolver;

            foreach (var bullet in pool.GetPooledObjects()) SetupBullet(bullet);
        }
    }
}