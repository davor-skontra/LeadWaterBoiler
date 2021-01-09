using System;

namespace DependencyInjection
{
    public interface IIocSubContainer
    {
        void Clear();
        void Unregister<TService>();
        void Unregister(Type type);
        void Register<TService>(TService service);
        void Register<TRegisterAs, TService>(TService service) where TService : TRegisterAs;
        object ResolveAnonymous(Type type);
        TType Resolve<TType>();
    }
}