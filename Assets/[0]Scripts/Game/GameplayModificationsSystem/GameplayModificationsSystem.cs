using UnityEngine;
using VContainer;


namespace Game
{
    internal sealed class GameplayModificationsSystem : MonoBehaviour
    {
        [SerializeField] private GameplayModification[] modifications;
        [Inject] private readonly IObjectResolver _resolver;

        private void Start ()
        {
            for (int i = 0; i < modifications.Length; i++)
            {
                var mod = modifications[i];

                _resolver.Inject(mod);
            }
        }
    }
}
