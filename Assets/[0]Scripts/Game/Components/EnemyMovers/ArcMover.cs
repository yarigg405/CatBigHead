using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Components
{
    internal sealed class ArcMover : MonoBehaviour, ITickable
    {
        [SerializeField] private float movingSpeed = 1f;
        [SerializeField] private float arcHeight = 20f;
        private float _verticalSpeed;
        private float _targetPosY;


        private void OnEnable()
        {
            _targetPosY = transform.position.y + arcHeight;
        }


        void ITickable.Tick(float deltaTime)
        {
            _verticalSpeed = (_targetPosY - transform.position.y) * 2f;

            var x = movingSpeed * deltaTime;
            var y = _verticalSpeed * deltaTime;


            transform.Translate(-x, y, 0);
        }
    }
}
