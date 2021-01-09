using System;

namespace DependencyInjection.Containers
{
    public class IocException : Exception
    {
        private IocException(string message) : base(message)
        {
        }

        public static IocException ShouldNotExist(Type type) =>
            new IocException($"{nameof(IocContainer)} already contains type {type})");

        public static IocException ShouldExist(Type type) =>
            new IocException($"{nameof(IocContainer)} does not contains type {type})");
    }
}