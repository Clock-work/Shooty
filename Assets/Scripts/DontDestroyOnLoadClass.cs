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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
