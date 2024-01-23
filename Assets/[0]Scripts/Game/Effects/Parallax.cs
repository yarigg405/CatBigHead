using Infrastructure.GameSystem;
using System;
using UnityEngine;
using VContainer;


namespace Game.Effects
{
    internal sealed class Parallax : MonoBehaviour, ITickable
    {
        [SerializeField] private float speedModifier = 1f;
        [SerializeField] private ParallaxSetting[] settings;
        
        [Inject] private readonly TickableProcessor _tickableProcessor;


        private void Awake()
        {
            _tickableProcessor.AddTickable(this);
        }

        private void OnDestroy()
        {
            _tickableProcessor.RemoveTickable(this);
        }

        void ITickable.Tick(float deltaTime)
        {
            for (int i = 0; i < settings.Length; i++)
            {
                var s = settings[i];
                var mat = s.Renderer.material;
                var offset = mat.mainTextureOffset;
                offset.x += deltaTime * s.Speed * speedModifier;
                mat.mainTextureOffset = offset;
            }
        }
    }

    [Serializable]
    internal sealed class ParallaxSetting
    {
        [field: SerializeField] public SpriteRenderer Renderer { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }

}
