using System;
using UnityEngine;
using UnityEngine.Events;


namespace Yrr.UI
{
    public abstract class UIScreen : MonoBehaviour
    {
        [Space]
        [SerializeField] private UnityEvent<object> onShow;
        [SerializeField] private UnityEvent onHide;

        private event Action ClosingCallback;
        [HideInInspector] public event Action<UIScreen> OnHideAction;



        public void Show(object args, Action callback)
        {
            ShowProcedure();
            ClosingCallback = callback;
            OnShow(args);
            onShow?.Invoke(args);
        }

        protected virtual void ShowProcedure()
        {
            gameObject.SetActive(true);
        }

        protected virtual void OnShow(object args) { }


        public void Hide()
        {
            if (!gameObject.activeSelf) return;

            OnHide();
            onHide?.Invoke();
            OnHideAction?.Invoke(this);
            ClosingCallback?.Invoke();
            ClosingCallback = null;
            HidingProcedure();
        }

        protected virtual void HidingProcedure()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnHide() { }
    }
}