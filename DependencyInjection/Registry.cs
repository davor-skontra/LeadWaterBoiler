using System;
using DependencyInjection.Containers;

namespace DependencyInjection
{
    public class Registry
    {
        private static ServiceContainer Services { get; } = new ServiceContainer();
        private static FactoryContainer Factories { get; } = new FactoryContainer();
        
        public void Clear()
        {
            Services.Clear();
            Factories.Clear();
        }

        public object ResolveAnonymous(Type type)
        {
            if (Services.Contains(type))
            {
                return Services.ResolveAnonymous(type);
            }

            if (Factories.Contains(type))
            {
                return Factories.ResolveAnonymous(type);
            }
            
            throw IocException.ShouldExist(type);
        }


        public void RegisterService<TService>(TService service)
        {
            var type = typeof(TService);
            
            if (TypeIsRegistered(type))
            {
                throw IocException.ShouldNotExist(type);
            }
            
            Services.Register<TService>(service);
        }

        public void RegisterFactory<TProduced>(Func<TProduced> factoryMethod) where TProduced: class
        {
            var type = typeof(TProduced);
            
            if (TypeIsRegistered(type))
            {
                throw IocException.ShouldNotExist(type);
            }
            
            Factories.Register<TProduced>(factoryMethod);
        }

        public void Unregister(Type type)
        {
            if (!TypeIsRegistered(type))
            {
                throw IocException.ShouldExist(type);
            }
        }

        public bool TypeIsRegistered(Type type) => Services.Contains(type) || Factories.Contains(type);
    }
}