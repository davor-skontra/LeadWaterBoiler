using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Utilities
{
    public class DisposeContainer: IDisposable
    {
        private List<IDisposable> _disposables = new List<IDisposable>();
        private List<Button.ButtonClickedEvent> _clickedEvents = new List<Button.ButtonClickedEvent>();

        public void Add(params IDisposable[] disposables)
        {
            foreach (var d in disposables)
            {
                Add(d);
            }
        }

        public void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
        
        public void Add(params Button.ButtonClickedEvent[] clickEvents)
        {
            foreach (var c in clickEvents)
            {
                Add(c);
            }
        }

        public void Add(Button.ButtonClickedEvent clickEvent)
        {
            _clickedEvents.Add(clickEvent);
        }

        public void Dispose()
        {
            foreach (var d in _disposables)
            {
                d.Dispose();
            }

            foreach (var c in _clickedEvents)
            {
                c.RemoveAllListeners();
            }
        }
    }
}