using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Components
{
    internal sealed class ArcMover : MonoBehaviour, ITickable
    {
        private float _targetPosY;
        private float _verticalSpeed;
        [SerializeField] private float arcHeight = 20f;
        [SerializeField] private float movingSpeed = 1f;


        void ITickable.Tick(float deltaTime)
        {
            _verticalSpeed = (_targetPosY - transform.position.y) * 2f;

            var x = movingSpeed * deltaTime;
            var y = _verticalSpeed * deltaTime;


            transform.Translate(-x, y, 0);
        }


        private void OnEnable()
        {
            _targetPosY = transform.position.y + arcHeight;
        }
    }
}