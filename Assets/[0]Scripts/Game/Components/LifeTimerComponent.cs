using Game.Components;
using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game
{
    internal sealed class LifeTimerComponent : MonoBehaviour, ITickable, IComponent
    {
        private float _currentLifeTime;
        [SerializeField] private Entity entity;
        [SerializeField] private float lifeTime = 3f;

        void ITickable.Tick(float deltaTime)
        {
            _currentLifeTime -= deltaTime;

            if (_currentLifeTime <= 0f) entity.GetEntityComponent<DestroyComponent>().Destroy();
        }


        private void OnEnable()
        {
            _currentLifeTime = lifeTime;
        }
    }
}