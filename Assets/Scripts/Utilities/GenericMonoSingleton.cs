using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
{
    public static T Instance { get { return instance; } }
    private static T instance;

    private void Awake()
    {
        if (instance == null)
            instance = (T)this;
        else
        {
            Destroy(this.gameObject);
            Debug.LogError("A duplicate Singleton of type " + (T)this + " was attempted to have been created");
        }
    }
}
