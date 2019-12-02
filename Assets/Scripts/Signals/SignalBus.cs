using System;
using System.Collections.Generic;

namespace Assets.Scripts.Signals
{
    public static class SignalBus
    {
        private static readonly Dictionary<Type, List<object>> Subscribers = new Dictionary<Type, List<object>>();

        public static void Subscribe<T>(Action<T> action)
        {
            var type = typeof(T);
            if (!Subscribers.TryGetValue(type, out var list))
            {
                Subscribers[type] = list = new List<object>();
            }

            list.Add(action);
        }

        public static void Unsubscribe<T>(Action<T> action)
        {
            var type = typeof(T);
            if (Subscribers.TryGetValue(type, out var list))
            {
                list.Remove(action);
            }
        }

        public static void Invoke<T>(T signal)
        {
            var type = typeof(T);
            if (Subscribers.TryGetValue(type, out var list))
            {
                foreach (var action in list)
                {
                    ((Action<T>)action)?.Invoke(signal);
                }
            }
        }
    }
}
