using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1;i<6;++i)
        {
            DefaultEnemy.createNewEnemy(0, 0, 1, 1, i, i * 2, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
