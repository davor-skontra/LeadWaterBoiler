using System;
using System.Collections.Generic;

namespace DependencyInjection.Containers
{
    public abstract class IocSubContainerBase<TResolvableType>
    {
        protected readonly Dictionary<Type, TResolvableType> _resolvables = new Dictionary<Type, TResolvableType>();
        
        public void Clear() => _resolvables.Clear();
        
        public void Unregister<TService>()
        {
            var type = typeof(TService);
                
            Unregister(type);
        }

        public void Unregister(Type type)
        {
            if (!_resolvables.ContainsKey(type))
            {
                throw IocException.ShouldExist(type);
            }

            _resolvables.Remove(type);
        }

        public object ResolveAnonymous(Type type)
        {
            if (_resolvables.ContainsKey(type))
            {
                return Resolver(type);
            }

            throw IocException.ShouldExist(type);
        }

        public bool Contains(Type type) => _resolvables.ContainsKey(type);
        
        public TType Resolve<TType>() => (TType) ResolveAnonymous(typeof(TType));
        
        protected abstract Func<Type, object> Resolver { get; }
    }
}