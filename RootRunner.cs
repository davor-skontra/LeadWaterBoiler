using System;
using UnityEngine;

public class RootRunner: MonoBehaviour
{
    [SerializeField] private CompositionRoot _compositionRoot;
    private void Awake()
    {
        _compositionRoot.Main();
    }
}