using System;
using System.Collections.Generic;
using UnityEngine;

namespace DependencyInjection.Containers
{
    public static class IocContainer
    {
        private static readonly Dictionary<Type, KnownType> _knownTypes = new Dictionary<Type, KnownType>();

        public static Registry Registry { get; } = new Registry();

        public static void Clear()
        {
            _knownTypes.Clear();
        }
        

        public static void Inject<TMonoBehaviour>(TMonoBehaviour self) where TMonoBehaviour : MonoBehaviour
        {
            var type = self.GetType();

            if (!_knownTypes.ContainsKey(type))
            {
                _knownTypes[type] = new KnownType(type);
            }

            _knownTypes[type].Resolve(self);
        }

        public static void Inject(object self)
        {
            var type = self.GetType();

            if (!_knownTypes.ContainsKey(type))
            {
                _knownTypes[type] = new KnownType(type);
            }

            _knownTypes[type].Resolve(self);
        }
    }
}