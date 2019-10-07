using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : DefaultEnemy
{
    public static DefaultEnemy createRandomEnemy(float sizeMin = 3f, float sizeMax = 5f, float speedMin = 2f, float speedMax = 20f, float rotationSpeedMin = -1.3f, float rotationSpeedMax = 1.3f, float minCooldown = 0.5f, float maxCooldown = 1.0f,
        float minProjectileSize = 1.0f, float maxProjectileSize = 1.5f, float minProjectileSpeed = 25f, float maxProjectileSpeed = 50f, int minProjectileDamage = 1, int maxProjectileDamage = 1)
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

        ShootingEnemy enemy =  (ShootingEnemy)createNewEnemy(position.x, position.y, widthHeight, widthHeight, targetX, targetY, speed, rotationSpeed, startDegrees, "Prefabs/Enemies/ShootingEnemy");
        enemy.m_cooldown = Random.Range(minCooldown, maxCooldown);
        enemy.m_projectileSize = Random.Range(minProjectileSize, maxProjectileSize);
        enemy.m_projectileSpeed = Random.Range(minProjectileSpeed, maxProjectileSpeed);
        enemy.m_projectileDamage = Random.Range(minProjectileDamage, maxProjectileDamage);
        return enemy;
    }

    private float m_time;
    private float m_cooldown;
    private float m_projectileSize;
    private float m_projectileSpeed;
    private int m_projectileDamage;

    protected override void onInit()
    {
        m_maxHealth = 3;
        m_points = 40;
    }

    override protected void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        m_time = 0;
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        m_time += 1 * Time.deltaTime;
        if(m_time>=m_cooldown)
        {
            Vector3 direction = (PlayerScript.instance.transform.position - this.transform.position).normalized;
            var projectilePos = new Vector2(transform.position.x, transform.position.y) + new Vector2(direction.x * transform.localScale.x / 2, direction.y * transform.localScale.y / 2);
            Projectile.createNewProjectile(projectilePos, new Vector2(m_projectileSize, m_projectileSize), this.transform.rotation, new Vector2(direction.x, direction.y), m_projectileSpeed, false, m_projectileDamage, 1);
            m_time = 0;
        }
    }
}
