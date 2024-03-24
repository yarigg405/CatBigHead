using Infrastructure.GameSystem;
using UnityEngine;


namespace Game
{
    internal sealed class SinusoidMover : MonoBehaviour, ITickable
    {
        private float _verticalSpeed;
        [SerializeField] private float speedX = 3f;
        [SerializeField] private float speedY = 1f;
        private bool _toDown;
        [SerializeField] private float verticalAcceleration = 5f;


        void ITickable.Tick(float deltaTime)
        {
            if (_toDown)
                _verticalSpeed -= verticalAcceleration * deltaTime;
            else
                _verticalSpeed += verticalAcceleration * deltaTime;
            CheckDirection();

            var x = speedX * deltaTime;
            var y = _verticalSpeed * deltaTime;

            transform.Translate(-x, y, 0);
        }

        private void CheckDirection()
        {
            if (_verticalSpeed >= speedY)
                _toDown = true;

            if (_verticalSpeed <= -speedY)
                _toDown = false;
        }
    }
}