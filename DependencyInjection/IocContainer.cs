using System;
using System.Collections.Generic;
using UnityEngine;

namespace DependencyInjection
{
    public static partial class IocContainer
    {
        private static readonly Dictionary<Type, KnownType> _knownTypes = new Dictionary<Type, KnownType>();
        public static ServiceContainer Services { get; } = new ServiceContainer();

        public static void Clear()
        {
            _knownTypes.Clear();
            Services.Clear();
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