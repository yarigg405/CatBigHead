using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace Yrr.UI.Elements
{
    public sealed class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] public UnityEvent OnClick;
        [SerializeField] bool _interactable = true;


        [Space]
        [Header("Visual")]
        [Space]

        [SerializeField] private UnityEvent OnNormal;
        [SerializeField] private UnityEvent OnPress;
        [SerializeField] private UnityEvent OnDisable;

        public event Action<CustomButtonStates> OnButtonStateChanged;
        private CustomButtonStates _currentState;

        private void OnEnable()
        {
            if (_interactable)
            {
                OnNormal?.Invoke();
                SetNewState(CustomButtonStates.Normal);
            }
            else
            {
                OnDisable?.Invoke();
                SetNewState(CustomButtonStates.Disabled);
            }
        }

        public bool interactable
        {
            get => _interactable;
            set
            {
                _interactable = value;
                if (_interactable)
                {
                    OnNormal?.Invoke();
                    SetNewState(CustomButtonStates.Normal);
                }
                else
                {
                    OnDisable?.Invoke();
                    SetNewState(CustomButtonStates.Disabled);
                }
            }
        }     


        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (!_interactable) return;

            OnPress?.Invoke();
            SetNewState(CustomButtonStates.Pressed);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (!_interactable) return;

            OnNormal?.Invoke();

            if (eventData.hovered.Contains(gameObject))
            {
                OnClick?.Invoke();
            }
        }

        internal void ClickOnButton()
        {
            if (!_interactable) return;

            OnClick?.Invoke();
        }

        private void SetNewState(CustomButtonStates newState)
        {
            if (newState == _currentState) return;

            _currentState = newState;
            OnButtonStateChanged?.Invoke(newState);
        }
    }
}
