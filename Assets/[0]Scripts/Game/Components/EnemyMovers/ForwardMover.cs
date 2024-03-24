using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Components
{
    internal sealed class ForwardMover : MonoBehaviour, ITickable
    {
        [SerializeField] private float speed = 1f;

        void ITickable.Tick(float deltaTime)
        {
            var tr = transform;
            var delta = tr.right * deltaTime * speed;
            var newPosition = tr.position - delta;
            tr.position = newPosition;
        }
    }
}