using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour {

    //Singleton
    private static GraphicsManager instance = null;

    public GameObject[] BubblePrefabs;

    public GameObject[] DestructionEffects;

    // Game Instance Singleton
    public static GraphicsManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        LoadResourcesToArray(ref BubblePrefabs, "Prefabs");
        LoadResourcesToArray(ref DestructionEffects, "Texture");
    }

    private static void LoadResourcesToArray<T>(ref T[] array, string folderName) where T:class
    {
        Debug.Log("Loadresources");
        Object[] temp = Resources.LoadAll(folderName);
        array = new T[temp.Length];
        Debug.Log("Temp length: " + temp.Length);
        for (int i = temp.Length - 1; i >= 0; --i)
        {
            array[i] = temp[i] as T;
        }
    }
}
