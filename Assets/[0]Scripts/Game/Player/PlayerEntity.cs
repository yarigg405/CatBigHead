using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Player
{
    internal sealed class PlayerEntity : TickableEntity
    {
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private BulletsSpawner shooterComponent;


        internal void Construct(PlayerInput input, TickableProcessor tickableProcessor)
        {
            playerMover.Construct(input);
            shooterComponent.Construct(tickableProcessor);
        }
    }
}
