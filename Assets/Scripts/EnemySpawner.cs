using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance = null;

    private class DefaultSpawner
    {
        private float m_sizeMin = 3f;
        private float m_sizeMax = 5f;
        private float m_speedMin = 2f;
        private float m_speedMax = 20f;
        private float m_rotationSpeedMin = -1.3f;
        private float m_rotationSpeedMax = 1.3f;
        private float m_spawnInterval = 1.0f;




    }


    private static List<DefaultSpawner> m_spawners;

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
