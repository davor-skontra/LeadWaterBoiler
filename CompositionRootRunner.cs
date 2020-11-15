using System;
using System.Linq;
using DependencyInjection;
using UnityEngine;

public class CompositionRootRunner: MonoBehaviour
{
    [SerializeField] private CompositionRoot _compositionRoot;
    private void Awake()
    {
        IocContainer.Inject(_compositionRoot);
        
        _compositionRoot.Main();
        
        foreach (var injectable in GetInjectableMonoBehaviours())
        {
            IocContainer.Inject(injectable);
        }
    }

    private MonoBehaviour[] GetInjectableMonoBehaviours() => gameObject
            .scene
            .GetRootGameObjects()
            .SelectMany(x => x.GetComponentsInChildren<MonoBehaviour>(true))
            .Where(x => x != _compositionRoot && x != this)
            .ToArray();
}