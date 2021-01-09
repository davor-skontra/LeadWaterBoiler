using System;
using System.Collections.Generic;
using DependencyInjection;
using DependencyInjection.Containers;
using UnityEngine;

public abstract class CompositionRoot : MonoBehaviour
{
    private readonly IocContainer.ServiceContainer _services = IocContainer.Services;
    
    private List<Type> _sceneBoundServiceTypes = new List<Type>();

    public abstract void Main();

    protected void RegisterService<TService>(TService service)
    {
        _sceneBoundServiceTypes.Add(typeof(TService));
        _services.Register<TService>(service);
    }

    private void ClearSceneBoundServices()
    {
        foreach (var type in _sceneBoundServiceTypes)
        {
            _services.Unregister(type);
        }
    }

    private void OnDestroy()
    {
        ClearSceneBoundServices();
    }
}