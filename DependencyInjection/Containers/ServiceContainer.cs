using System;
using System.Collections.Generic;

namespace DependencyInjection.Containers
{
    public class ServiceContainer : IocSubContainerBase<object>
    {
        protected override Func<Type, object> Resolver => t => _resolvables[t];
        
        public void Register<TService>(TService service)
        {
            var type = typeof(TService);

            if (_resolvables.ContainsKey(type))
            {
                throw IocException.ShouldNotExist(type);
            }

            _resolvables[type] = service;
        }

        public void Register<TRegisterAs, TResolvable>(TResolvable service) where TRegisterAs : TResolvable
        {
            Register((TRegisterAs) service);
        }
    }
}