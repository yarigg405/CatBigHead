using Game.Components;
using UnityEngine;
using VContainer;


namespace Game
{
    [CreateAssetMenu(fileName = "CatBigHatMod", menuName = "Modifications/CatBigHatMod", order = 51)]
    internal sealed class CatBigHatMod : GameplayModification
    {
        [Inject]
        private void Construct(PlayerProvider playerProvider)
        {
            playerProvider.Player.GetEntityComponent<HatChangerComponent>().ShowHat();
        }
    }
}
