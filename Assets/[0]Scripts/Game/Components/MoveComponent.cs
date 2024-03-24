using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Components
{
    internal sealed class MoveComponent : MonoBehaviour, ITickable, IComponent
    {
        private Vector3 _moveDirection;
        [SerializeField] private float moveSpeed = 5f;

        void ITickable.Tick(float deltaTime)
        {
            transform.position += _moveDirection * moveSpeed * deltaTime;
        }


        internal void Move(Vector2 direction)
        {
            _moveDirection = direction;
        }
    }
}