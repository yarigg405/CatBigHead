using Game.Components;
using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game
{
    internal sealed class LifeTimerComponent : MonoBehaviour, ITickable
    {
        [SerializeField] private Entity entity;
        [SerializeField] private float lifeTime = 3f;
        private float _currentLifeTime = 0f;


        private void OnEnable()
        {
            _currentLifeTime = lifeTime;
        }

        void ITickable.Tick(float deltaTime)
        {
            _currentLifeTime -= deltaTime;

            if(_currentLifeTime <= 0f)
            {
                entity.Get<DestroyComponent>().Destroy();
            }
        }
    }
}
