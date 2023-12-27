using Game.Components;
using Game.Entities;
using Infrastructure.GameSystem;
using UnityEngine;


namespace Game.Player
{
    internal sealed class PlayerEntity : TickableEntity
    {
        [SerializeField] private PlayerMover playerMover;


        internal void Construct(PlayerInput input, TickableProcessor tickableProcessor)
        {
            playerMover.Construct(input);

            var needTickables = GetComponentsInChildren<INeedTickableProcessor>();
            foreach (var needTickable in needTickables)
            {
                needTickable.SetTickableProcessor(tickableProcessor);
            }
        }
    }
}
