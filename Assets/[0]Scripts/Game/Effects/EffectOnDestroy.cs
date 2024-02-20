using Game.Components;
using Game.Entities;
using UnityEngine;


namespace Game
{
    internal sealed class EffectOnDestroy : MonoBehaviour
    {
        [SerializeField] private Entity controlledEntity;
        [SerializeField] private EffectsSpawner effectsSpawner;



        private void Awake()
        {
            controlledEntity.Get<DestroyComponent>().OnDestroy += ShowEffect;
        }

        private void OnDestroy()
        {
            controlledEntity.Get<DestroyComponent>().OnDestroy -= ShowEffect;
        }

        private void ShowEffect()
        {
            effectsSpawner.SpawnEffect();
        }
    }
}
