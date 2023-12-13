using Game.Components;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Player
{
    internal sealed class PlayerMover : MonoBehaviour, ITickable
    {
        [SerializeField] internal MoveComponent moveComponent;
        private PlayerInput _input;

        internal void Construct(PlayerInput input)
        {
            _input = input;
        }
      
        void ITickable.Tick(float deltaTime)
        {
            moveComponent.Move(_input.InputData);
        }
    }
}
