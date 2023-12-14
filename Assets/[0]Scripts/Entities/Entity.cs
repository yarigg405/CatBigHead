using System;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Entities
{
    internal abstract class Entity : MonoBehaviour, IEntity
    {
        private readonly Dictionary<Type, object> _components = new();

        public T Get<T>()
        {
            return (T)_components[typeof(T)];
        }

        public bool TryGet<T>(out T element)
        {
            if (_components.TryGetValue(typeof(T), out var result))
            {
                element = (T)result;
                return true;
            }

            element = default(T);
            return false;
        }

        public void Add(object component)
        {
            _components.Add(component.GetType(), component);
        }

        public void Add(object component, Type componentType)
        {
            _components.Add(componentType, component);
        }
    }
}
