using DG.Tweening;
using UnityEngine;


namespace Yrr.UI.Animators
{
    internal sealed class ColorRendererAnimator : TweenAnimator
    {
        [SerializeField] private MeshRenderer mRenderer;
        private Color _defaultColor;
        [SerializeField] private Color endColor;

        [SerializeField] private float durationUp;
        [SerializeField] private float durationDown;


        private float durationToStartColor = 0.35f;
        [SerializeField] private Color customStartColor = Color.white;



        private void Awake()
        {
            _defaultColor = mRenderer.material.color;
        }



        protected override Sequence GetSequence()
        {
            mRenderer.material.color = _defaultColor;
            var seq = DOTween.Sequence(this).SetUpdate(true);

            if (durationToStartColor > 0)
            {
                seq
                    .Append(DOVirtual.Color(_defaultColor, customStartColor, durationToStartColor, (value) =>
                    {
                        mRenderer.material.color = value;
                    }))
                    ;
            }

            seq
                .Append(DOVirtual.Color(_defaultColor, endColor, durationUp, (value) =>
                {
                    mRenderer.material.color = value;
                }))
                .Append(DOVirtual.Color(endColor, _defaultColor, durationDown, (value) =>
                {
                    mRenderer.material.color = value;
                }))
                ;

            return seq;
        }

        protected override void ResetToDefault()
        {
            if (durationDown < 0)
                mRenderer.material.color = endColor;
            else
                mRenderer.material.color = _defaultColor;
        }
    }
}
