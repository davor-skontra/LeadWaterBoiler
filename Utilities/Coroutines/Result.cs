using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Coroutines;

public class Result<TReturned>
{
    private readonly ICoroutineRunner _runner;
    
    private bool _hasBeenSet;
    private TReturned _return;

    public Result(ICoroutineRunner runner)
    {
        _runner = runner;
        Await = runner.StartCoroutine(WaitRoutine());
    }

    public TReturned Return
    {
        get
        {
            if (!_hasBeenSet)
            {
                throw Exception.ResultNotSet();
            }
            return _return;
        }
        set
        {
            _hasBeenSet = true;
            _return = value;
        }
    }
    
    public Coroutine Await { get; }

    public IEnumerator WaitRoutine()
    {
        while (!_hasBeenSet)
        {
            yield return null;
        }
    }

    public class Exception : System.Exception
    {
        private Exception(string message) : base(message)
        {
            
        }

        public static Exception ResultNotSet() => 
            new Exception($"Result for type {typeof(TReturned)} has not been set yet");
    }
}
