using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;

public abstract class Singletone<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get => _instance;
        
        protected set
        {
            if (_instance)
            {
                Destroy(value.gameObject);
                return;
                
            }
            _instance = value;
        }
    }

    private void Awake()
    {
        OnAwake();
    }

    public abstract void OnAwake();
}
