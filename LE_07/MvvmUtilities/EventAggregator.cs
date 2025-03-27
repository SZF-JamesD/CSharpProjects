using System;
using System.Collections.Generic;


namespace MvvmUtilities
{
    public class EventAggregator
    {
        private readonly Dictionary<Type, List<Action<object>>> _subscribers = new Dictionary<Type, List<Action<object>>>();

        public void Subscribe<T>(Action<T> action)
        {
            if (!_subscribers.ContainsKey(typeof(T))) _subscribers[typeof(T)] = new List<Action<object>>();

            _subscribers[typeof(T)].Add(obj => action((T)obj));
        }

        public void Publish<T>(T message)
        {
            if (_subscribers.TryGetValue(typeof(T), out var actions))
                foreach (var action in actions)
                    action(message);
        }

        public void Unsubscribe<T>(Action<T> action)
        {
            if (_subscribers.TryGetValue(typeof(T), out var actions))
                actions.RemoveAll(a => a.Equals((Action<object>)(o => action((T)o))));
        }
    }
}
