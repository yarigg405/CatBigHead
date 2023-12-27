using UnityEngine;


namespace Game.Components
{
    internal sealed class LinearMover : MonoBehaviour
    {
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private Vector2 startMovingDirection = new Vector2(-1f, 0f);


        private void OnEnable()
        {
            moveComponent.Move(startMovingDirection);
        }
    }
}
