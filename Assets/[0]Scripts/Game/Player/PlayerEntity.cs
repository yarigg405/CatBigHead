using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Player
{
    internal sealed class PlayerEntity : Entity
    {
        [SerializeField] private PlayerMover playerMover;
        private TickableProcessor _tickableProcessor;


        internal void Construct(PlayerInput input, TickableProcessor tickableProcessor)
        {
            playerMover.Construct(input);
            _tickableProcessor = tickableProcessor;

            var tickables = GetComponentsInChildren<ITickable>();
            foreach (var tickable in tickables)
            {
                _tickableProcessor.AddTickable(tickable);
            }
        }
    }
}
