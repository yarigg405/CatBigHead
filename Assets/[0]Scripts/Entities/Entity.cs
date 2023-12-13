using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    internal abstract class Entity : MonoBehaviour, IEntity
    {
        private readonly List<object> _components = new();

        public T Get<T>()
        {
            foreach (var component in _components)
            {
                if (component is T result)
                    return result;
            }

            throw new Exception($"Element of type {typeof(T).Name} is not found!");
        }

        public bool TryGet<T>(out T element)
        {
            foreach (var component in _components)
            {
                if (component is T result)
                {
                    element = result;
                    return true;
                }
            }
            element = default;
            return false;
        }

        public void Add(object component)
        {
            _components.Add(component);
        }
    }
}
