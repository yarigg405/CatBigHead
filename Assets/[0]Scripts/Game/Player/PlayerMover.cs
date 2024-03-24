using Game.Components;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;

namespace Game.Player
{
    internal sealed class PlayerMover : MonoBehaviour, ITickable
    {
        private PlayerInput _input;
        [SerializeField] internal MoveComponent moveComponent;

        void ITickable.Tick(float deltaTime)
        {
            moveComponent.Move(_input.InputData);
        }

        [Inject]
        internal void Construct(PlayerInput playerInput)
        {
            _input = playerInput;
        }
    }
}