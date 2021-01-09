using System;
using System.Collections.Generic;

namespace DependencyInjection
{
    public static partial class IocContainer
    {
        public class ServiceContainer : IIocSubContainer
        {
            private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

            public void Clear() => _services.Clear();

            public void Unregister<TService>()
            {
                var type = typeof(TService);
                
                Unregister(type);
            }

            public void Unregister(Type type)
            {
                if (!_services.ContainsKey(type))
                {
                    throw ServiceLocatorException.ShouldExist(type);
                }

                _services.Remove(type);
            }

            public void Register<TService>(TService service)
            {
                var type = typeof(TService);

                if (_services.ContainsKey(type))
                {
                    throw ServiceLocatorException.ShouldNotExist(type);
                }

                _services[type] = service;
            }

            public void Register<TRegisterAs, TService>(TService service) where TService : TRegisterAs
            {
                Register((TRegisterAs) service);
            }

            public object ResolveAnonymous(Type type)
            {
                if (_services.ContainsKey(type))
                {
                    return _services[type];
                }

                throw ServiceLocatorException.ShouldExist(type);
            }

            public TType Resolve<TType>() => (TType) ResolveAnonymous(typeof(TType));

            public class ServiceLocatorException : Exception
            {
                private ServiceLocatorException(string message) : base(message)
                {
                }

                public static ServiceLocatorException ShouldNotExist(Type type) =>
                    new ServiceLocatorException($"Service Locator already contains type {type})");

                public static ServiceLocatorException ShouldExist(Type type) =>
                    new ServiceLocatorException($"Service Locator does not contains type {type})");
            }
        }
    }
}