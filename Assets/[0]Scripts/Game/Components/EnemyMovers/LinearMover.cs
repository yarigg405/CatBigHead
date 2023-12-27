using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Components
{
    internal sealed class LinearMover : MonoBehaviour, ITickable
    {
        [SerializeField] private Vector2 velocity = new Vector2(-1f, 0f);
        private Vector3 _velocity;


        private void OnEnable()
        {
            _velocity = velocity;
        }

        void ITickable.Tick(float deltaTime)
        {
            transform.position = transform.position + _velocity * deltaTime;
        }
    }
}
