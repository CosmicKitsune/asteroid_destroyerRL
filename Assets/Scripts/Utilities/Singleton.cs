using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //note to self: this is called an auto property, that's why it's a property but doesnt relate to any variable: it is itself both the property and variable.
    public static T instance { get; private set; }

    //whenever using Awake in scripts that inherit from Singleton<>, remember to call base.Awake() first and foremost.
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(this);
        }
    }

    /*protected override void Awake()
    {
        base.Awake();
    }*/ // This is if u want awake in a script inheriting singleton
}