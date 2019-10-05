﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : MonoBehaviour
{
    public static List<DefaultEnemy> enemies = new List<DefaultEnemy>();

    private float m_moveSpeed;
    private Rigidbody2D m_rigidbody;
    private CircleCollider2D m_collider;

    public static DefaultEnemy createRandomEnemy()
    {
        var cameraMinPos = DefaultCamera.instance.getMinPos();
        var cameraMaxPos = DefaultCamera.instance.getMaxPos();

        float scale = Random.Range(3f, 5f);

        float width = scale;
        float height = scale;
        float x = Random.Range(cameraMinPos.x + width, cameraMaxPos.x - width);
        float y = Random.Range(cameraMinPos.y + height, cameraMaxPos.y - height);
        float speed = Random.Range(0.5f, 5f);
        float targetX = Random.Range(-10f, 10f);
        float targetY = Random.Range(-10f, 10f);

        return createNewEnemy(x, y, width, height, targetX, targetY, speed, "DefaultEnemy");
    }

    public static DefaultEnemy createNewEnemy(float x, float y, float width, float height, float targetX, float targetY, float moveSpeed, string prefabName)
    {
        return createNewEnemy(new Vector2(x, y), new Vector2(width, height), new Vector2(targetX, targetY), moveSpeed, prefabName);
    }
    public static DefaultEnemy createNewEnemy(Vector2 position, Vector2 size, Vector2 direction, float moveSpeed, string prefabName)
    {
        var gameobject = Instantiate((GameObject)Resources.Load(prefabName), new Vector3(position.x, position.y, 0), Quaternion.identity);
        gameobject.transform.localScale = new Vector3(size.x, size.y, 1);
        var enemy = gameobject.GetComponent<DefaultEnemy>();
        enemy.m_rigidbody.velocity = direction.normalized;
        enemy.m_moveSpeed = moveSpeed;
        return enemy;
    }

    private void Awake()
    {
        if (!enemies.Contains(this))
        {
            enemies.Add(this);
        }
        m_rigidbody = this.GetComponent<Rigidbody2D>();
        m_collider = this.GetComponent<CircleCollider2D>();
        m_rigidbody.isKinematic = false;
        m_rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    private void OnDestroy()
    {
        if(enemies.Contains(this))
        {
            enemies.Remove(this);
        }

    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }




}
