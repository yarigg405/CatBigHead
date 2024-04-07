using System.Collections.Generic;


namespace Game
{
    internal sealed class ModificationsBlackboard
    {
        private readonly Dictionary<string, object> _variables = new();

        public T GetVariable<T>(string key)
        {
            return (T)_variables[key];
        }

        public bool TryGetVariable<T>(string key, out T value)
        {
            if (_variables.TryGetValue(key, out var result))
            {
                value = (T)result;
                return true;
            }

            value = default;
            return false;
        }

        public void SetVariable(string key, object value)
        {
            _variables[key] = value;
        }
    }
}
