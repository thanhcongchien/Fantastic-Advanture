using System.Collections.Generic;
using System;
using UnityEngine;

public static class ServiceLocator
{
    //Todo Recheck why use this attribute
    /*[ThreadStatic]*/
    private static List<IServiceWrapper> serviceWrapperList;

    public static void Set<T>(T instance)
    {
        if (ServiceWrapper<T>.instance != null)
        {
            Debug.LogError("An instance of this service class has already been set! " + instance.GetType());
            return;
        }
        ServiceWrapper<T>.instance = instance;
        RegisterServiceWrapperList();

        ServiceLocator.serviceWrapperList.Add(new ServiceWrapper<T>());
    }

    public static void Replace<T>(T instance)
    {
        if (ServiceLocator.IsSet<T>())
        {
            ServiceLocator.Unset<T>();
        }
        ServiceLocator.Set<T>(instance);
    }

    public static void SetWithoutAutoUnLoad<T>(T instance)
    {
        if (ServiceWrapper<T>.instance != null)
        {
            Debug.LogError("An instance of this service class has already been set! " + instance.GetType());
            return;
        }

        ServiceWrapper<T>.instance = instance;
        RegisterServiceWrapperList();

        ServiceLocator.serviceWrapperList.Add(new ServiceWrapper<T>());
    }

    public static void ReplaceWithoutAutoUnLoad<T>(T instance)
    {
        if (ServiceLocator.IsSet<T>())
        {
            ServiceLocator.Unset<T>();
        }
        if (ServiceWrapper<T>.instance != null)
        {
            Debug.LogError("An instance of this service class has already been set! " + instance.GetType());
            return;
        }

        ServiceWrapper<T>.instance = instance;
        RegisterServiceWrapperList();

        ServiceLocator.serviceWrapperList.Add(new ServiceWrapper<T>());
    }

    //        public static void Set<T>(T instance) {
    //            Set(instance,true);
    //        }

    private static void RegisterServiceWrapperList()
    {
        if (ServiceLocator.serviceWrapperList == null)
        {
            ServiceLocator.serviceWrapperList = new List<IServiceWrapper>();
        }
    }

    public static T Get<T>()
    {
        var tInstance = ServiceWrapper<T>.instance;
        if (tInstance == null)
        {
            //var instance = Type.GetType(typeof(T).ToString());
            //Debug.LogError("Instance Null " + typeof(T)+", start create new");
            //Set(Type.GetType(typeof(T).ToString()));
            //tInstance = ServiceWrapper<T>.instance;
            Debug.LogError("Instance Null " + typeof(T));
        }

        return tInstance;
    }
    public static T Get<T>(T instance)
    {
        var tInstance = ServiceWrapper<T>.instance;
        if (tInstance == null)
        {
            try
            {
                try
                {
                    ServiceWrapper<T>.instance = default(T);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
                ServiceWrapper<T>.instance = instance;
                tInstance = ServiceWrapper<T>.instance;
                Debug.LogError("Instance Null " + typeof(T) + ", start create new");
            }
            catch
            {
                Set(instance);
                tInstance = instance;
                Debug.LogError("Instance Null " + typeof(T) + ", start create new");
            }

        }

        return tInstance;
    }
    public static IGameUIManager GetUIViewManager => Get<GameUIManager>();
    /// <summary>
    /// Use instead of Get<T> when called by reflection method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetReflection<T>()
    {
        return Get<T>();
    }

    public static T Get<T>(bool autoSet)
    {
        if (IsSet<T>() == true)
        {
        }
        else if (autoSet)
        {
            Set<T>(default(T));
        }

        return Get<T>();
    }

    public static bool IsSet<T>()
    {
        return ServiceWrapper<T>.instance != null;
    }

    public static void ResetAll()
    {
#if UNITY_EDITOR
#endif
        if (ServiceLocator.serviceWrapperList == null)
        {
            return;
        }

        foreach (var instance in serviceWrapperList)
        {
            instance.Unset();
        }

        ServiceLocator.serviceWrapperList = null;
    }

    public static void Unset<T>()
    {
        try
        {
            ServiceWrapper<T>.instance = default(T);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public static T SetAndGet<T>(T instance)
    {
        Set(instance);
        return ServiceWrapper<T>.instance;
    }
}
internal interface IServiceWrapper
{
    void Unset();
}

internal class ServiceWrapper<T> : IServiceWrapper
{
    public static T instance = default(T);

    public void Unset()
    {
        ServiceWrapper<T>.instance = default(T);
    }
}