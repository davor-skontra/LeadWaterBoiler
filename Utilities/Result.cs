using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Result<TReturned>
    {
    
        private bool _hasBeenSet;
        private TReturned _return;
        private List<Action<TReturned>> _notifyResultActions = new List<Action<TReturned>>();
    
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
                foreach (var notifyResult in _notifyResultActions)
                {
                    notifyResult(value);
                }
            }
        }

        public Result<TNextReturned> Then<TNextReturned>(Func<TReturned, Result<TNextReturned>> onTimeForNextResult)
        {
            var nextResult = new Result<TNextReturned>();
            
            _notifyResultActions.Add(OnResultReady);

            return nextResult;

            void OnResultReady(TReturned returned)
            {
                onTimeForNextResult(returned)
                    .Finally(x => nextResult.Return = x);
            }
        }

        public void Finally(Action<TReturned> onResultReady)
        {
            if (_hasBeenSet)
            {
                onResultReady(_return);
            }
            else
            {
                _notifyResultActions.Add(onResultReady);
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
