using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance = null;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        for (int i = 0; i < 2; ++i)
        {
            DefaultEnemy.createRandomEnemy();
            ShootingEnemy.createRandomEnemy();
            SeekingEnemy.createRandomEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.K))
        {
            DefaultEnemy.createRandomEnemy();
            ShootingEnemy.createRandomEnemy();
            SeekingEnemy.createRandomEnemy();
        }
    }
}
