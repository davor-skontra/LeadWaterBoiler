using System;
using System.Collections.Generic;

namespace DependencyInjection.Containers
{
    public class ServiceContainer : IocSubContainerBase<object>
    {
        protected override Func<Type, object> Resolver => t => _contained[t];
    }
}