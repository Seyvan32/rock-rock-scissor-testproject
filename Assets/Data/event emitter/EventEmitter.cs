using System;
using System.Collections.Generic;
using UnityEngine;

// EventEmitter class  
public class EventEmitter
{
    private readonly Dictionary<string, List<Action<dynamic>>> _events = new();

    public void on(string eventKey, Action<dynamic> listener)
    {
        if (!_events.ContainsKey(eventKey))
        {
            _events[eventKey] = new List<Action<dynamic>>();
        }
        _events[eventKey].Add(listener);
    }

    public void off(string eventName, Action<dynamic> listener)
    {
        if (!_events.ContainsKey(eventName)) return;

        _events[eventName].Remove(listener);
    }

    public void emit(string eventKey)
    {
        if (_events.ContainsKey(eventKey))
        {
            foreach (var listener in _events[eventKey])
            {
                Debug.Log("Emitting event: " + eventKey);
            }
        }
    }
}

// Mixin Utility  
public static class EventEmitterMixin
{
    public static TBase CreateEventEmitter<TBase>(TBase baseClass) where TBase : class
    {
        dynamic emitter = new EventEmitter();
        var mixin = baseClass;

        var mixedClass = new
        {
            Mixin = emitter,
            Original = mixin
        };

        // Returning the mixed class (dynamic to allow flexible structure)  
        return mixedClass as TBase;
    }
}

// EventEmitterComponent extending the Component class with event functionality  
public class EventEmitterComponent : MonoBehaviour
{
    private readonly EventEmitter _eventEmitter = new EventEmitter();

    public void on(string eventName,Action<dynamic> listener) => _eventEmitter.on(eventName, listener);

    public void off(string eventName, Action<dynamic> listener) => _eventEmitter.off(eventName, listener);

    public void emit(string eventName) => _eventEmitter.emit(eventName);
}