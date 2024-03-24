using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Components
{
    internal sealed class SimpleUpDownMover : MonoBehaviour, ITickable
    {
        [SerializeField] private float maxYCoordinate = 2;
        [SerializeField] private float minYCoordinate = -2;
        [SerializeField] private float movingSpeedX = 1;
        [SerializeField] private float movingSpeedY = 1;
        [SerializeField] private bool toDown = true;

        void ITickable.Tick(float deltaTime)
        {
            CheckDirection();

            var deltaY = toDown ? -movingSpeedY : movingSpeedY;
            var deltaX = -movingSpeedX;

            transform.Translate(deltaX * deltaTime, deltaY * deltaTime, 0);
        }

        private void CheckDirection()
        {
            if (transform.position.y > maxYCoordinate)
                toDown = true;

            if (transform.position.y < minYCoordinate)
                toDown = false;
        }
    }
}