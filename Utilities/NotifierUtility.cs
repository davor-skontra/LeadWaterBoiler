using System;
using System.Collections.Generic;

namespace Utilities
{
    public interface INotifier<TListener>
    {
        List<TListener> Listeners { get; }
    }

    public static class NotifierUtility
    {
        public static void NotifyListeners<TListener>(this INotifier<TListener> self, Action<TListener> action)
        {
            foreach (var listener in self.Listeners)
            {
                action(listener);
            }
        }

        public static void AddListener<TListener>(this INotifier<TListener> self, TListener listener)
        {
            if (self.Listeners.Contains(listener))
            {
                throw Exception.AlreadyContains(self.GetType(), listener.GetType());
            }

            self.Listeners.Add(listener);
        }

        public static void RemoveListener<TListener>(this INotifier<TListener> self, TListener listener)
        {
            self.Listeners.Remove(listener);
        }

        public static void RemoveAllListeners<TListener>(this INotifier<TListener> self)
        {
            self.Listeners.Clear();
        }

        public class Exception : System.Exception
        {
            private Exception(string message) : base(message)
            {
            }

            public static Exception AlreadyContains(Type notifier, Type listener) =>
                new Exception($"{notifier} already has a listener of type {listener}");
        }
    }
}