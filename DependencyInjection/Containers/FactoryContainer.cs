using System;

namespace DependencyInjection.Containers
{
    public class FactoryContainer: IocSubContainerBase<Func<object>>
    {
        protected override Func<Type, object> Resolver => (t) => _resolvables[t].Invoke();

        public void Register<TProduced>(Func<TProduced> service) where TProduced : class
        {
            var type = typeof(TProduced);

            if (_resolvables.ContainsKey(type))
            {
                throw IocException.ShouldNotExist(type);
            }

            _resolvables[type] = service;
        }

        public void Register<TRegisterAs, TResolvable>(Func<TRegisterAs> service) 
            where TRegisterAs : class, TResolvable
        {
            Register<TRegisterAs>(service);
        }
    }
}