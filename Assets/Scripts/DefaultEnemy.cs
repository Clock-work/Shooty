using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : MonoBehaviour
{
    public static List<DefaultEnemy> enemies = new List<DefaultEnemy>();

    [SerializeField]
    public Sprite[] sprites;

    [SerializeField]
    public SpriteRenderer renderer;

    private int indicie = 0;
    public float time = 0;

    [SerializeField]
    public float AnimationInterval = 2f;

    protected Rigidbody2D m_rigidbody;
    protected Collider2D m_collider;
    protected int m_health = 1;
    protected int m_maxHealth = 1;
    protected float m_rotationSpeed;
    protected float m_startDegrees;
    //maybe change for bosses
    protected int m_directDamage = 1;

    private const int maxNumberOfSpawnTries = 10;

    protected float m_points;

    public void Animate()
    {
        if(time > AnimationInterval)
        {
            indicie++;
            renderer.sprite = sprites[indicie % 3];
            time = 0;
        }
    }

    //returns false if no free position was found
    public static bool findNewFreePosition(float width, float height, out Vector2 position)
    {
        var minPos = BoundsManager.getInternalMinPos();
        var maxPos = BoundsManager.getInternalMaxPos();


        for(int i = 0;i< maxNumberOfSpawnTries;++i)
        {
            float x = Random.Range(minPos.x + width / 2, maxPos.x - width / 2);
            float y = Random.Range(minPos.y + height / 2, maxPos.y - height / 2);
            Bounds bounds = new Bounds(new Vector3(x, y, -1), new Vector3(width, height, 1));
            bool collided = false;
            foreach(var enemy in enemies)
            {
                if(enemy.wouldCollide(bounds))
                {
                    collided = true;
                    break;
                }
            }

            if(collided || PlayerScript.instance.wouldCollide(bounds))
            {
                continue;
            }

            position = new Vector2(x, y);
            return true;
        }
        position = new Vector2(0, 0);
        return false;
    }

    //returns null if there is no space for another enemy | creates a new defaultenemy
    public static DefaultEnemy createRandomEnemy(float sizeMin = 3f, float sizeMax = 5f, float speedMin = 2f, float speedMax = 20f, float rotationSpeedMin = -1.3f, float rotationSpeedMax = 1.3f)
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

        return createNewEnemy(position.x, position.y, widthHeight, widthHeight, targetX, targetY, speed, rotationSpeed, startDegrees, "Prefabs/Enemies/DefaultEnemy");
    }

    public static DefaultEnemy createNewEnemy(float x, float y, float width, float height, float targetX, float targetY, float moveSpeed, float rotationSpeed, float startDegrees, string prefabName)
    {
        return createNewEnemy(new Vector2(x, y), new Vector2(width, height), new Vector2(targetX, targetY), moveSpeed, rotationSpeed, startDegrees, prefabName);
    }
    public static DefaultEnemy createNewEnemy(Vector2 position, Vector2 size, Vector2 direction, float moveSpeed, float rotationSpeed, float startDegrees, string prefabName)
    {
        var gameobject = Instantiate((GameObject)Resources.Load(prefabName), new Vector3(position.x, position.y, -1), Quaternion.identity);
        gameobject.transform.localScale = new Vector3(size.x, size.y, 1);
        var enemy = gameobject.GetComponent<DefaultEnemy>();
        enemy.m_rigidbody.velocity = moveSpeed * direction.normalized;
        enemy.m_rotationSpeed = rotationSpeed;
        enemy.transform.Rotate(new Vector3(0,0,1),startDegrees);
        enemy.onInit();
        return enemy;
    }

    //special values per subclass
    protected virtual void onInit()
    {
        m_maxHealth = 1;
        m_points = 10;
    }

    //called after enemy is destroyed and removed
    protected virtual void onDeath()
    {
        if(m_points>0)
        {
            PlayerScript.instance.update.points += (int)m_points;
        }
    }

    //checks for potential collision with the point
    protected virtual bool wouldCollide(Bounds bounds)
    {
        return m_collider.bounds.Intersects(bounds);
    }

    public void attackMe(int damage)
    {
        m_health -= damage;
        if (m_health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    virtual protected void Awake()
    {
        if (!enemies.Contains(this))
        {
            enemies.Add(this);
        }
        m_rigidbody = this.GetComponent<Rigidbody2D>();
        m_collider = this.GetComponent<Collider2D>();
        m_rigidbody.isKinematic = false;
        m_rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        m_health = m_maxHealth;
    }

    protected void OnDestroy()
    {
        if(enemies.Contains(this))
        {
            enemies.Remove(this);
        }
        this.onDeath();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        Animate();
        time += Time.deltaTime;

        if(m_health<=0)
        {
            Destroy(this.gameObject);
        }
        this.transform.Rotate(new Vector3(0, 0, 1), m_rotationSpeed);

        m_points -= 0.3f * m_points  * Time.deltaTime;

    }

    virtual protected void FixedUpdate()
    {

    }

    //always call this in subclass if the method has to be overriden again
    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("player"))
        {
            var player = collision.gameObject.GetComponent<PlayerScript>();
            player.attackMe(m_directDamage);
            Destroy(this.gameObject);
        }
    }




}
