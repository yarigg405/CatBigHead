using System;
using UnityEngine;


namespace Yrr.Utils
{
    [Serializable]
    public class ReactiveValue<T>
    {
        public event Action<T> OnChange;
        [SerializeField]
        private T _currentValue;

        public T Value
        {
            get => _currentValue;
            set => SetValue(value);
        }



        public ReactiveValue()
        {
            _currentValue = default(T);
        }

        public ReactiveValue(T startValue)
        {
            _currentValue = startValue;
        }

        protected virtual void SetValue(T value)
        {
            if (value.Equals(_currentValue)) return;

            _currentValue = value;
            OnChange?.Invoke(_currentValue);
        }


        public static implicit operator T(ReactiveValue<T> value)
        {
            return value.Value;
        }



        public override bool Equals(object obj)
        {
            return Value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
