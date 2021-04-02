using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:class
{
    public static T instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            Initialise();
        }
        else
        { 
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    protected virtual void Initialise() { }
}
