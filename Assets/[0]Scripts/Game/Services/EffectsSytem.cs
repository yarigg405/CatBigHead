using Game.Components;
using Game.Entities;
using Game.Fx;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;


namespace Game
{
    internal sealed class EffectsSytem : MonoBehaviour
    {
        private TickableProcessor _tickableProcessor;
        [SerializeField] private EffectsPool pool;

        internal void PrepareEffect(EffectEntity effectPrefab)
        {
            var count = pool.GetCountOf(effectPrefab);
            if (count == 0)
            {
                pool.PopulateWith(effectPrefab, 3);
            }
        }

        internal Entity SpawnEffect(EffectEntity effectPrefab)
        {
            var effect = pool.SpawnObject(effectPrefab, null);
            return effect;
        }


        [Inject]
        private void Construct(TickableProcessor processor)
        {
            foreach (var bullet in pool.GetPooledObjects()) SetupEffect(bullet);

            pool.OnNewObjectInstantiated += SetupEffect;
            _tickableProcessor = processor;
        }

        private void SetupEffect(EffectEntity effect)
        {
            if (effect.TryGetEntityComponent<DestroyComponent>(out var dstr)) return;

            effect.SetupEntity();

            effect.Construct();
            _tickableProcessor.AddTickable(effect);
            effect.GetEntityComponent<DestroyComponent>().OnDestroy += () => pool.DespawnObject(effect);
        }
    }
}
