using System;
using System.Collections.Generic;
using DependencyInjection;
using DependencyInjection.Containers;
using UnityEngine;

public abstract class CompositionRoot : MonoBehaviour
{
    private readonly Registry _registry = IocContainer.Registry;
    
    private List<Type> _sceneBoundRegisteredTypes = new List<Type>();

    public abstract void Main();

    public virtual void AfterInject()
    {
        
    }

    protected void RegisterService<TService>(TService service)
    {
        _sceneBoundRegisteredTypes.Add(typeof(TService));
        _registry.RegisterService<TService>(service);
    }
    
    protected void RegisterFactory<TProduced>(Func<TProduced> factoryMethod) where TProduced: class
    {
        _sceneBoundRegisteredTypes.Add(typeof(TProduced));
        _registry.RegisterFactory<TProduced>(factoryMethod);
    }

    private void ClearSceneBoundServices()
    {
        foreach (var type in _sceneBoundRegisteredTypes)
        {
            _registry.Unregister(type);
        }
    }

    private void OnDestroy()
    {
        ClearSceneBoundServices();
    }
}