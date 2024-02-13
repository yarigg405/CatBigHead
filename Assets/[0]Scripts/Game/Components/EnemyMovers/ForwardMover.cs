using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Components
{
    internal sealed class ForwardMover : MonoBehaviour, ITickable
    {
        [SerializeField] private float speed = 1f;

        void ITickable.Tick(float deltaTime)
        {
            transform.position = transform.position - transform.right * deltaTime * speed;
        }
    }
}
