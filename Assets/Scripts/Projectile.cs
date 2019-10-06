using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour//todo: maybe also homing projectiles without initial velocity, but updating it 
{
    private Rigidbody2D m_rigidbody;

    private bool m_friendly;
    private int m_damage;
    private int m_charges;


    public static Projectile createNewProjectile(Vector2 position, Vector2 size, Quaternion rotation, Vector2 direction, float moveSpeed, bool friendly, int damage, int charges)
    {
        string prefabName = friendly ? "PlayerProjectile" : "EnemyProjectile";
        var gameobject = Instantiate((GameObject)Resources.Load("Prefabs/"+prefabName), new Vector3(position.x, position.y, -1), rotation);
        gameobject.transform.localScale = new Vector3(size.x, size.y, 1);
        var projectile = gameobject.GetComponent<Projectile>();
        projectile.m_friendly = friendly;
        projectile.m_damage = damage;
        projectile.m_charges = charges;
        projectile.m_rigidbody.velocity = moveSpeed * direction.normalized;
        projectile.onInit();
        return projectile;
    }

    protected void onInit()
    {

    }

    //called after projectile is destroyed and removed | todo: animations, etc
    protected virtual void onDeath()
    {

    }

    private void Awake()
    {
        m_rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDestroy()
    {
        this.onDeath();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void checkCharges()
    {
        if (--m_charges <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("bounds"))
        {
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag.Equals("player") && !m_friendly)
        {
            collision.gameObject.GetComponent<PlayerScript>().attackMe(m_damage);
            checkCharges();
        }
        else if(collision.gameObject.tag.Equals("enemy") && m_friendly)
        {
            collision.gameObject.GetComponent<DefaultEnemy>().attackMe(m_damage);
            checkCharges();
        }
    }


}
