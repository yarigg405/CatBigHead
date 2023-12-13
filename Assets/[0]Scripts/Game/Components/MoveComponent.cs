using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Components
{
    internal sealed class MoveComponent : MonoBehaviour, ITickable
    {
        [SerializeField] private float moveSpeed = 5f;
        private Vector3 _moveDirection;


        internal void Move(Vector2 direction)
        {
            _moveDirection = direction;
        }

        void ITickable.Tick(float deltaTime)
        {
            transform.position = transform.position + _moveDirection * moveSpeed * deltaTime;
        }
    }
}
