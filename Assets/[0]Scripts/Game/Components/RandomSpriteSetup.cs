using UnityEngine;
using Yrr.Utils;


namespace Game
{
    internal sealed class RandomSpriteSetup : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] sprites;

        private void OnEnable()
        {
            spriteRenderer.sprite = sprites.GetRandomItem();
        }
    }
}
