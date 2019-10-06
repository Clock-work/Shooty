using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingEnemy : DefaultEnemy
{
    new public static DefaultEnemy createRandomEnemy(float sizeMin = 3f, float sizeMax = 5f, float speedMin = 2f, float speedMax = 20f, float rotationSpeedMin = -1.3f, float rotationSpeedMax = 1.3f, float minCooldown = 0.5f, float maxCooldown = 1.0f)
    {
        float widthHeight = Random.Range(sizeMin, sizeMax);

        if (!findNewFreePosition(widthHeight, widthHeight, out Vector2 position))
        {
            return null;
        }

        float speed = Random.Range(speedMin, speedMax);
        float targetX = Random.Range(-10f, 10f);
        float targetY = Random.Range(-10f, 10f);

        float rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
        if (rotationSpeed < 0.05f && rotationSpeed > -0.05f)
        {
            if (rotationSpeed > 0)
            {
                rotationSpeed += 0.1f;
            }
            else
            {
                rotationSpeed -= 0.1f;
            }
        }
        float startDegrees = Random.Range(0.0f, 360.0f);

        SeekingEnemy enemy = (SeekingEnemy)createNewEnemy(position.x, position.y, widthHeight, widthHeight, targetX, targetY, speed, rotationSpeed, startDegrees, "Prefabs/Enemies/SeekingEnemy");
        enemy.m_cooldown = Random.Range(minCooldown, maxCooldown);
        enemy.m_speedMin = speedMin;
        enemy.m_speedMax = speedMax;
        return enemy;
    }

    private float m_time;
    private float m_cooldown;
    private float m_speedMin;
    private float m_speedMax;

    protected void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        m_time = 0;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
        m_time += 1 * Time.deltaTime;
        if (m_time >= m_cooldown)
        {
            Vector3 direction = (PlayerScript.instance.transform.position - this.transform.position).normalized;
            m_rigidbody.velocity = Random.Range(m_speedMin, m_speedMax) * direction.normalized;
            m_time = 0;
        }
    }
}
