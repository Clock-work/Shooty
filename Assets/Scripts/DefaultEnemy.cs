using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : MonoBehaviour
{
    public static List<DefaultEnemy> enemies = new List<DefaultEnemy>();

    private float m_moveSpeed;
    private Rigidbody2D m_rigidbody;
    private BoxCollider2D m_collider;

    public static DefaultEnemy createNewEnemy(float x, float y, float width, float height, float targetX, float targetY, float moveSpeed)
    {
        return createNewEnemy(new Vector2(x, y), new Vector2(width, height), new Vector2(targetX, targetY), moveSpeed);
    }
    public static DefaultEnemy createNewEnemy(Vector2 position, Vector2 size, Vector2 direction, float moveSpeed)
    {
        var gameobject = Instantiate((GameObject)Resources.Load("DefaultEnemy"), new Vector3(position.x, position.y, 0), Quaternion.identity);
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
        m_collider = this.GetComponent<BoxCollider2D>();
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




}
