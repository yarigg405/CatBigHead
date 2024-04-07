using UnityEngine;


namespace Game.Components
{
    internal sealed class HatChangerComponent : MonoBehaviour, IComponent
    {
        [SerializeField] private GameObject hat;

        internal void ShowHat()
        {
            hat.SetActive(true);
        }

        internal void HideHat()
        {
            hat.SetActive(false);
        }
    }
}
