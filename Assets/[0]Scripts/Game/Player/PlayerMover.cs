using Game.Components;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;


namespace Game.Player
{
    internal sealed class PlayerMover : MonoBehaviour, ITickable, IComponent
    {        
        [SerializeField] internal MoveComponent moveComponent;
        private PlayerInput _input;

        internal bool IsInverted { get; set; }

        void ITickable.Tick(float deltaTime)
        {
            if (IsInverted)
            {
                moveComponent.Move(-_input.InputData);
            }

            else
            {
                moveComponent.Move(_input.InputData);
            }
        }

        [Inject]
        private void Construct(PlayerInput playerInput)
        {
            _input = playerInput;
        }
    }
}