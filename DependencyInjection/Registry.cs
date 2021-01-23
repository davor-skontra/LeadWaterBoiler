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

        public object ResolveAnonymous(Type type) =>
            Services.Contains(type) 
                ? Services.ResolveAnonymous(type) 
                : Factories.ResolveAnonymous(type);
        

        public void RegisterService<TService>(TService service)
        {
            Services.Register<TService>(service);
        }

        public void RegisterFactory<TProduced>(Func<TProduced> factoryMethod) where TProduced: class
        {
            Factories.Register<TProduced>(factoryMethod);
        }

        public void Unregister(Type type)
        {
            throw new NotImplementedException();
        }
    }
}