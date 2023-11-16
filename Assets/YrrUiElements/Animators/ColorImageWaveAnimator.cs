using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Yrr.UI.Animators;


namespace Yrr.Ui.Animators
{
    internal sealed class ColorImageWaveAnimator : TweenAnimator
    {
        [SerializeField] private Image colorableImage;
        [SerializeField] private Color startColor;
        [SerializeField] private Color endColor;

        [SerializeField] private float duration;

        protected override Sequence GetSequence()
        {
            colorableImage.color = startColor;
            var seq = DOTween.Sequence(this).SetUpdate(true)
                .Append(DOVirtual.Color(startColor, endColor, duration, (value) =>
                {
                    colorableImage.color = value;
                }))
                .Append(DOVirtual.Color(endColor, startColor, duration, (value) =>
                {
                    colorableImage.color = value;
                }));

            return seq;
        }

        protected override void ResetToDefault()
        {
            colorableImage.color = startColor;
        }
    }
}