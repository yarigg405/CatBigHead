using Game.Components;
using Game.Fx;
using UnityEngine;
using VContainer;


namespace Game
{
    internal sealed class EffectsSpawner : MonoBehaviour, IComponent
    {
        [SerializeField] private EffectEntity effectPrefab;
        private EffectsSytem _effectsSystem;


        [Inject]
        private void Construct(EffectsSytem effectsSystem)
        {
            _effectsSystem = effectsSystem;
            _effectsSystem.PrepareEffect(effectPrefab);
        }

        internal void SpawnEffect()
        {
            var effect = _effectsSystem.SpawnEffect(effectPrefab);
            effect.transform.position = transform.position;
        }
    }
}