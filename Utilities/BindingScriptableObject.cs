using System;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public abstract class BindingScriptableObject<TKey, TValue>: ScriptableObject
    {
        [SerializeField] private Binding[] _bindings;
        
        public Binding[] Bindings => _bindings;

        public TValue Get(TKey key) => Bindings.First(x => x.Key.Equals(key)).Value;
        
        public TValue GetOrDefault(TKey key, TValue defaultValue)
        {
            try
            {
                return Bindings.First(x => x.Key.Equals(key)).Value;
            }
            catch (InvalidOperationException e)
            {
                return defaultValue;
            }
        }

        [Serializable]
        public class Binding
        {
            [SerializeField] private TKey _key;
            [SerializeField] private TValue _value;

            public TKey Key => _key;

            public TValue Value => _value;
        }
    }
}