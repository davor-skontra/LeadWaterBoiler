using System;
using System.Collections.Generic;

namespace DependencyInjection.Containers
{
    public abstract class IocSubContainerBase<TContainedType>
    {
        protected readonly Dictionary<Type, TContainedType> _contained = new Dictionary<Type, TContainedType>();
        
        public void Clear() => _contained.Clear();


        public void Unregister<TService>()
        {
            var type = typeof(TService);
                
            Unregister(type);
        }

        public void Unregister(Type type)
        {
            if (!_contained.ContainsKey(type))
            {
                throw IocException.ShouldExist(type);
            }

            _contained.Remove(type);
        }

        public void Register<TContained>(TContained service) where TContained: TContainedType
        {
            var type = typeof(TContained);

            if (_contained.ContainsKey(type))
            {
                throw IocException.ShouldNotExist(type);
            }

            _contained[type] = service;
        }

        public void Register<TRegisterAs, TContained>(TContained service) where TRegisterAs : TContained, TContainedType
        {
            Register((TRegisterAs) service);
        }

        public object ResolveAnonymous(Type type)
        {
            if (_contained.ContainsKey(type))
            {
                return Resolver(type);
            }

            throw IocException.ShouldExist(type);
        }
        
        public TType Resolve<TType>() => (TType) ResolveAnonymous(typeof(TType));
        
        protected abstract Func<Type, object> Resolver { get; }
    }
}