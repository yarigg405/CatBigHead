using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Components
{
    internal sealed class LinearMover : MonoBehaviour, ITickable
    {
        private Vector3 _velocity;
        [SerializeField] private Vector2 velocity = new(-1f, 0f);

        void ITickable.Tick(float deltaTime)
        {
            transform.position += _velocity * deltaTime;
        }


        private void OnEnable()
        {
            _velocity = velocity;
        }
    }
}