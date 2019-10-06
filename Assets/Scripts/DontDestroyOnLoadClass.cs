using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadClass : MonoBehaviour
{
    public static int SecondsAlive = 100;
    public static DontDestroyOnLoadClass instance;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}