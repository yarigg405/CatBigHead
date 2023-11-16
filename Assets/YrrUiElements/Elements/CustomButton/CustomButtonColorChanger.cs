using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Yrr.UI.Elements
{
    internal sealed class CustomButtonColorChanger : MonoBehaviour
    {
        [SerializeField] private CustomButton customButton;

        [Space]
        [SerializeField] private Image[] imagesForRecolor;
        [SerializeField] private TextMeshProUGUI[] tmpsForRecolor;

        [Space]
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color pressedColor = Color.white;
        [SerializeField] private Color disabledColor = new Color(0.78f, 0.78f, 0.78f, 0.5f);


        private void Awake()
        {
            SetNormal();
            customButton.OnButtonStateChanged += ChangeColor;
        }

        private void OnDestroy()
        {
            customButton.OnButtonStateChanged -= ChangeColor;
        }

        private void ChangeColor(CustomButtonStates state)
        {
            switch (state)
            {
                case CustomButtonStates.Normal:
                    SetNormal(); break;

                case CustomButtonStates.Pressed:
                    SetPressed(); break;

                case CustomButtonStates.Disabled:
                    SetDisabled(); break;
            }
        }

        private void SetNormal()
        {
            foreach (var image in imagesForRecolor)
            {
                image.color = normalColor;
            }

            foreach (var tmp in tmpsForRecolor)
            {
                tmp.color = normalColor;
            }
        }

        private void SetPressed()
        {
            foreach (var image in imagesForRecolor)
            {
                image.color = pressedColor;
            }

            foreach (var tmp in tmpsForRecolor)
            {
                tmp.color = pressedColor;
            }
        }

        private void SetDisabled()
        {
            foreach (var image in imagesForRecolor)
            {
                image.color = disabledColor;
            }

            foreach (var tmp in tmpsForRecolor)
            {
                tmp.color = disabledColor;
            }
        }
    }
}
