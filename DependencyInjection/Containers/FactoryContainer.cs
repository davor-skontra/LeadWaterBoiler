using System;

namespace DependencyInjection.Containers
{
    public class FactoryContainer: IocSubContainerBase<Func<object>>
    {
        protected override Func<Type, object> Resolver => (t) => _contained[t].Invoke();
    }
}