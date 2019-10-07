using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance = null;

    private class DefaultSpawner
    {
        protected float m_sizeMin;
        protected float m_sizeMax;
        protected float m_speedMin;
        protected float m_speedMax;
        protected float m_rotationSpeedMin;
        protected float m_rotationSpeedMax;
        protected float m_spawnInterval;

        private float m_counter = 0;
        private float m_delay;

        public DefaultSpawner(float delay)
        {
            m_delay = delay;
            m_counter = m_spawnInterval;
            m_rotationSpeedMin = -1.5f;
            m_rotationSpeedMax = 1.5f;
            m_sizeMin = 1.5f;
            m_sizeMax = 6f;

            m_speedMin = 5f;
            m_speedMax = 10f;


            m_spawnInterval = 5.0f;

        }

        public void update()
        {
            m_counter += 1 * Time.fixedDeltaTime;
            if(m_counter<m_delay)
            {
                return;
            }
            else if (m_delay > 0)
            {
                m_delay = 0;
                m_counter = 0;
            }
            if(m_counter>=m_spawnInterval)
            {
                m_counter = 0;
                this.spawn();
            }
        }
        virtual protected void spawn()
        {
            DefaultEnemy.createRandomEnemy(m_sizeMin, m_sizeMax, m_speedMin, m_speedMax, m_rotationSpeedMin, m_rotationSpeedMax);
            if(m_spawnInterval>2f)
            {
                m_spawnInterval *= 0.9625f;
            }
            if (m_speedMax < 50.0f)
            {
                m_speedMin *= 1.0375f;
                m_speedMax *= 1.0375f;
            }
        }

    }

    private class SeekingSpawner
    : DefaultSpawner
    {
        protected float m_cooldownMin;
        protected float m_cooldownMax;

        public SeekingSpawner(float delay)
             : base(delay)
        {
            m_speedMin = 8f;
            m_speedMax = 16f;

            m_cooldownMin = 0.5f;
            m_cooldownMax = 1f;

            m_spawnInterval = 10.0f;
        }
        override protected void spawn()
        {
            SeekingEnemy.createRandomEnemy(m_sizeMin, m_sizeMax, m_speedMin, m_speedMax, m_rotationSpeedMin, m_rotationSpeedMax, m_cooldownMin, m_cooldownMax);
            if (m_spawnInterval > 2f)
            {
                m_spawnInterval *= 0.875f;
            }

            if (m_speedMax < 40.0f)
            {
                m_speedMin *= 1.1f;
                m_speedMax *= 1.1f;
            }

            if (m_cooldownMax > 0.2f)
            {
                m_cooldownMin *= 0.9f;
                m_cooldownMax *= 0.9f;
            }

        }


    }

    private class ShootingSpawner
        : DefaultSpawner
    {
        protected float m_cooldownMin;
        protected float m_cooldownMax;
        protected float m_projectileSizeMin;
        protected float m_projectileSizeMax;
        protected float m_projecdtileSpeedMin;
        protected float m_projecdtileSpeedMax;

        public ShootingSpawner (float delay)
            : base(delay)
        {
            m_speedMin = 5f;
            m_speedMax = 10f;

            m_cooldownMin = 0.5f;
            m_cooldownMax = 1f;
            m_projectileSizeMin = 0.5f;
            m_projectileSizeMax = 2f;
            m_projecdtileSpeedMin = 25f;
            m_projecdtileSpeedMax = 50f;

            m_spawnInterval = 15.0f;
        }

        override protected void spawn()
        {
            ShootingEnemy.createRandomEnemy(m_sizeMin, m_sizeMax, m_speedMin, m_speedMax, m_rotationSpeedMin, m_rotationSpeedMax, m_cooldownMin, m_cooldownMax, m_projectileSizeMin, m_projectileSizeMax, m_projecdtileSpeedMin, m_projecdtileSpeedMax);
            if (m_spawnInterval > 2f)
            {
                m_spawnInterval *= 0.825f;
            }

            if (m_speedMax < 30.0f)
            {
                m_speedMin *= 1.15f;
                m_speedMax *= 1.15f;
            }

            if (m_cooldownMax > 0.5f)
            {
                m_cooldownMin *= 0.85f;
                m_cooldownMax *= 0.85f;
            }

            if (m_projectileSizeMax < 8f)
            {
                m_projectileSizeMin *= 1.15f;
                m_projectileSizeMax *= 1.15f;
            }

            if (m_projecdtileSpeedMax < 150f)
            {
                m_projecdtileSpeedMin *= 1.15f;
                m_projecdtileSpeedMax *= 1.15f;
            }

        }

    }



    private List<DefaultSpawner> m_spawners;
    
    private void Awake()
    {
        instance = this;
        m_spawners = new List<DefaultSpawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_spawners.Add(new DefaultSpawner(1f));
        m_spawners.Add(new SeekingSpawner(11f));
        m_spawners.Add(new ShootingSpawner(21f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(var spawner in m_spawners)
        {
            spawner.update();
        }
    }
}
