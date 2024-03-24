using System;
using UnityEngine;


namespace Game.Components
{
    internal sealed class TeamComponent : MonoBehaviour, IComponent
    {
        [field: SerializeField] public Team Team { get; private set; }
    }

    [Serializable]
    public enum Team
    {
        Player,
        Enemy
    }
}