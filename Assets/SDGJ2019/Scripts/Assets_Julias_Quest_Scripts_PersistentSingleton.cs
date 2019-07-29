using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PersistentSingleton<T> : MonoBehaviour where T : Component
{
    protected static T _instance;
    protected bool _enabled;
    static bool isExitingGame = false;
    static Object instanceLock = new Object();
    /// <summary>
    /// Singleton design pattern
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (isExitingGame)
            {
                return null;
            }
            lock (instanceLock)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        //obj.hideFlags = HideFlags.HideAndDontSave;
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }

        }
    }
    private void OnApplicationQuit()
    {
        isExitingGame = true;
    }
    /// <summary>
    /// On awake, we check if there's already a copy of the object in the scene. If there's one, we destroy it.
    /// </summary>
    protected virtual void Awake()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this as T;
            DontDestroyOnLoad(transform.gameObject);
            _enabled = true;
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
