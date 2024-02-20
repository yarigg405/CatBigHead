using Game.Components;
using Game.Fx;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game
{
    internal sealed class EffectsSpawner : MonoBehaviour, INeedTickableProcessor
    {
        [SerializeField] private EffectsPool pool;
        private TickableProcessor _tickableProcessor;

        internal void SpawnEffect()
        {
            var effect = pool.SpawnObject();
            effect.transform.position = transform.position;
        }

        void INeedTickableProcessor.SetTickableProcessor(TickableProcessor processor)
        {
            pool.OnNewObjectInstantiated += SetupEffect;
            _tickableProcessor = processor;

            foreach (var bullet in pool.GetPooledObjects())
            {
                SetupEffect(bullet);
            }
        }

        private void SetupEffect(EffectEntity effect)
        {
            effect.Construct();
            _tickableProcessor.AddTickable(effect);
            effect.Get<DestroyComponent>().OnDestroy += () => pool.DespawnObject(effect);
        }
    }
}
