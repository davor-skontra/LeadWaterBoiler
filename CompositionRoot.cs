using DependencyInjection;
using UnityEngine;

namespace AlkarInjector
{
    public abstract class CompositionRoot : MonoBehaviour
    {
        protected readonly IocContainer.ServiceContainer Services = IocContainer.Services;
        private void Start()
        {
            Main();
        }

        protected abstract void Main();
    }
}