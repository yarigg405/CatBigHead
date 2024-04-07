using Infrastructure.GameSystem;
using System;
using UnityEngine;


namespace Game.Components
{
    internal sealed class ShootTimerComponent : MonoBehaviour, ITickable, IComponent
    {
        private int _currentShotsCount;

        private float _currentTimeBetweenBursts;
        private float _currentTimeBetweenShots;
        [SerializeField] private int shotsCountInBurst = 3;
        [SerializeField] private float timeBetweenBursts = 1f;
        [SerializeField] private float timeBetweenShots = 0.2f;

        public float ShotTimeModificator { get; set; } = 1f;

        void ITickable.Tick(float deltaTime)
        {
            if (!enabled) return;
            if (_currentTimeBetweenBursts > 0)
                HandleTimeBetweenBursts(deltaTime);

            else if (_currentTimeBetweenShots > 0) HandleTimeBetweenShots(deltaTime);
        }

        internal event Action OnShoot;


        private void OnEnable()
        {
            ResetShooting();
        }

        private void ResetShooting()
        {
            _currentTimeBetweenBursts = timeBetweenBursts * ShotTimeModificator;
            _currentTimeBetweenShots = timeBetweenShots;
            _currentShotsCount = shotsCountInBurst;
        }

        private void HandleTimeBetweenBursts(float deltaTime)
        {
            _currentTimeBetweenBursts -= deltaTime;
        }

        private void HandleTimeBetweenShots(float deltaTime)
        {
            _currentTimeBetweenShots -= deltaTime;
            if (_currentTimeBetweenShots < 0)
                Shot();
        }

        private void Shot()
        {
            _currentShotsCount--;
            OnShoot?.Invoke();

            if (_currentShotsCount <= 0)
                ResetShooting();

            else
                _currentTimeBetweenShots = timeBetweenShots;
        }
    }
}