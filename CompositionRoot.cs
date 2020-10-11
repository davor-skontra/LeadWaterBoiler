using System;
using System.Collections.Generic;
using DependencyInjection;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class CompositionRoot : MonoBehaviour
{
    private readonly IocContainer.ServiceContainer Services = IocContainer.Services;
    private List<Type> _sceneBoundServiceTypes = new List<Type>();
        
    private void Start()
    {
        Main();
    }

    protected abstract void Main();

    protected void RegisterService<TService>(TService service)
    {
        _sceneBoundServiceTypes.Add(typeof(TService));
        Services.Register<TService>(service);
    }

    private void ClearSceneBoundServices()
    {
        foreach (var type in _sceneBoundServiceTypes)
        {
            Services.Unregister(type);
        }
    }

    private void OnDestroy()
    {
        ClearSceneBoundServices();
    }
}