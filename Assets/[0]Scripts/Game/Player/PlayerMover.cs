using Game.Components;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;


namespace Game.Player
{
    internal sealed class PlayerMover : MonoBehaviour, ITickable
    {
        [SerializeField] internal MoveComponent moveComponent;
        private PlayerInput _input;

        [Inject]
        internal void Construct(PlayerInput playerInput)
        {
            _input = playerInput;
        }

        void ITickable.Tick(float deltaTime)
        {
            moveComponent.Move(_input.InputData);
        }
    }
}
