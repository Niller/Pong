using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> Services;

    static ServiceLocator()
    {
        Services = new Dictionary<Type, object>();
    }

    public static object Register(Type type, object instance)
    {
        if (Services.TryGetValue(type, out var oldInstance) && oldInstance is IDisposable disposable)
        {
            disposable.Dispose();
        }
        return Services[type] = instance;
    }

    public static T Register<T>(T instance)
    {
        return (T) Register(typeof(T), instance);
    }

    public static object Get(Type type)
    {
        if (Services.TryGetValue(type, out var instance))
        {
            return instance;
        }

        throw new ArgumentException($"Cannot find service with type {type}");
    }

    public static bool TryGet<T>(out T instance)
    {
        var result = Services.TryGetValue(typeof(T), out var instance1);
        instance = (T) instance1;
        return result;
    }

    public static T Get<T>()
    {
        return (T) Get(typeof(T));
    }

    public static void Remove<T>()
    {
        var type = typeof(T);
        if (Services.TryGetValue(type, out var instance) && instance is IDisposable disposable)
        {
            disposable.Dispose();
        }

        Services.Remove(type);
    }
}