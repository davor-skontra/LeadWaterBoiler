using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Coroutines
{
    public class Result<TReturned>
    {
    
        private bool _hasBeenSet;
        private TReturned _return;
        private List<Action<TReturned>> notifyResultActions = new List<Action<TReturned>>();
    
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
                if (_hasBeenSet)
                {
                    throw Exception.ResultAlreadySet();
                }
                
                _hasBeenSet = true;
                _return = value;
                foreach (var notifyResult in notifyResultActions)
                {
                    notifyResult(value);
                }
            }
        }

        public void Then(Action<TReturned> onResultReady)
        {
            if (_hasBeenSet)
            {
                onResultReady(_return);
            }
            else
            {
                notifyResultActions.Add(onResultReady);
            }
        }

        public CustomYieldInstruction Await => new WaitUntil(() => _hasBeenSet);

        public class Exception : System.Exception
        {
            private Exception(string message) : base(message)
            {
            
            }

            public static Exception ResultNotSet() => 
                new Exception($"Result for type {typeof(TReturned)} has not been set yet");
            
            public static Exception ResultAlreadySet() =>
                new Exception($"Result for type {typeof(TReturned)} has already been set yet");

        }
    }

    public class Nothing
    {
        // No result
    }
}
