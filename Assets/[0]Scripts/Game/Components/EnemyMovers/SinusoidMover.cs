using Infrastructure.GameSystem;
using UnityEngine;


namespace Game
{
    internal sealed class SinusoidMover : MonoBehaviour, ITickable
    {
        [SerializeField] private float speedX = 3f;
        [SerializeField] private float speedY = 1f;
        [SerializeField] private float verticalAcceleration = 5f;

        private float _verticalSpeed;
        private bool toDown;


        void ITickable.Tick(float deltaTime)
        {
            if (toDown)
                _verticalSpeed -= verticalAcceleration * deltaTime;
            else
                _verticalSpeed += verticalAcceleration * deltaTime;
            CheckDirection();

            float x = speedX * deltaTime;
            float y = _verticalSpeed * deltaTime;

            transform.Translate(-x, y, 0);
        }

        private void CheckDirection()
        {
            if (_verticalSpeed >= speedY)
                toDown = true;

            if (_verticalSpeed <= -speedY)
                toDown = false;
        }
    }
}
