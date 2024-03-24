using Game.Components;
using Game.Fx;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;

namespace Game
{
    internal sealed class EffectsSpawner : MonoBehaviour
    {
        private TickableProcessor _tickableProcessor;
        [SerializeField] private EffectsPool pool;

        internal void SpawnEffect()
        {
            var effect = pool.SpawnObject();
            effect.transform.position = transform.position;
        }

        [Inject]
        private void Construct(TickableProcessor processor)
        {
            pool.OnNewObjectInstantiated += SetupEffect;
            _tickableProcessor = processor;

            foreach (var bullet in pool.GetPooledObjects()) SetupEffect(bullet);
        }

        private void SetupEffect(EffectEntity effect)
        {
            effect.Construct();
            _tickableProcessor.AddTickable(effect);
            effect.GetEntityComponent<DestroyComponent>().OnDestroy += () => pool.DespawnObject(effect);
        }
    }
}