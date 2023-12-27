using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Components
{
    internal sealed class SimpleUpDownMover : MonoBehaviour, ITickable
    {
        [SerializeField] private float movingSpeedX = 1;
        [SerializeField] private float movingSpeedY = 1;
        [SerializeField] private float maxYcoordinate = 2;
        [SerializeField] private float minYcoordinate = -2;
        [SerializeField] private bool toDown = true;

        void ITickable.Tick(float deltaTime)
        {
            CheckDirection();

            float deltaY = toDown ? -movingSpeedY : movingSpeedY;
            float deltaX = -movingSpeedX;

            transform.Translate(deltaX * deltaTime, deltaY * deltaTime, 0);
        }

        private void CheckDirection()
        {
            if (transform.position.y > maxYcoordinate)
                toDown = true;

            if (transform.position.y < minYcoordinate)
                toDown = false;
        }
    }
}
