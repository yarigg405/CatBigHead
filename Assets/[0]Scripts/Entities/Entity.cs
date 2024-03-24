using System;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Entities
{
    internal abstract class Entity : MonoBehaviour, IEntity
    {
        private readonly Dictionary<Type, object> _components = new();

        public T GetEntityComponent<T>()
        {
            return (T)_components[typeof(T)];
        }

        public bool TryGetEntityComponent<T>(out T element)
        {
            if (_components.TryGetValue(typeof(T), out var result))
            {
                element = (T)result;
                return true;
            }

            element = default;
            return false;
        }

        public void AddEntityComponent(object component)
        {
            _components.Add(component.GetType(), component);
        }

        public void AddEntityComponent(object component, Type componentType)
        {
            _components.Add(componentType, component);
        }
    }
}