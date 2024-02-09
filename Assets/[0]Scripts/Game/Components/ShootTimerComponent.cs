using Infrastructure.GameSystem;
using System;
using UnityEngine;


namespace Game.Components
{
    internal sealed class ShootTimerComponent : MonoBehaviour, ITickable
    {
        [SerializeField] private float timeBetweenBursts = 1f;
        [SerializeField] private int shotsCountInBurst = 3;
        [SerializeField] private float timeBetweenShots = 0.2f;

        private float _currentTimeBetweenBursts = 0f;
        private float _currentTimeBetweenShots = 0;
        private int _currentShotsCount = 0;

        internal event Action OnShoot;


        private void OnEnable()
        {
            ResetShooting();
        }

        private void ResetShooting()
        {
            _currentTimeBetweenBursts = timeBetweenBursts;
            _currentTimeBetweenShots = timeBetweenShots;
            _currentShotsCount = shotsCountInBurst;
        }

        void ITickable.Tick(float deltaTime)
        {
            if (!enabled) return;
            if (_currentTimeBetweenBursts > 0)
            {
                HandleTimeBetweenBursts(deltaTime);
            }

            else if (_currentTimeBetweenShots > 0)
            {
                HandleTimeBetweenShots(deltaTime);
            }
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
            {
                _currentTimeBetweenShots = timeBetweenShots;
            }
        }
    }
}
