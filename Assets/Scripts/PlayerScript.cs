using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    enum ANIMATIONSTATE
    {
        IDLE = 0,
        MOVE_01 = 1,
        MOVE_02 = 2,
        IDLE_SHOOT_01 = 3,
        IDLE_SHOOT_02 = 4,
        MOVE_SHOOT_01 = 5,
        MOVE_SHOOT_02 = 6
    };

    [SerializeField]
    public Text Health;

    [SerializeField]
    public float movementSpeed = 1.0f;

    [SerializeField]
    public float boostFaktor = 2.0f;

    [SerializeField]
    public Rigidbody2D rigidbody;

    [SerializeField]
    private Vector2 movement = new Vector2(0, 0);

    [SerializeField]
    private Vector2 mousePosition = new Vector2(0, 0);

    [SerializeField]
    public Sprite[] sprites;

    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    public UpdateScript update;

    [SerializeField]
    private float seconds = 0;


    public static PlayerScript instance;

    private Collider2D m_collider;

    private const float playerSafeAreaRadius = 15;

    private AudioSource m_shootSound;


    private int m_health;
    private int m_maxHealth;

    private float m_shootCooldown;
    private float m_time = 0;

    private void Awake()
    {
        instance = this;
        m_collider = this.gameObject.GetComponent<Collider2D>();
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        update = this.gameObject.GetComponent<UpdateScript>();
        m_shootSound = this.gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = update.movementSpeed;
        m_shootCooldown = update.fireRate;
        m_maxHealth = 3;
        m_health = m_maxHealth;
    }

    public void ReloadStats(float movementSpeed, float fireRate)
    {
        this.movementSpeed = movementSpeed;
        this.m_shootCooldown = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        Health.text = m_health.ToString();

        seconds += Time.deltaTime;

        if(Input.GetMouseButton(0) == true)
        {
            shootProjectile();
            if(movement == Vector2.zero)
            {
                int random = Random.Range(0, 2);
                renderer.sprite = sprites[(int)ANIMATIONSTATE.IDLE_SHOOT_01 + random];
            }
            else
            {
                int random = Random.Range(0, 2);
                renderer.sprite = sprites[(int)ANIMATIONSTATE.MOVE_SHOOT_01 + random];
            }
        }
        else
        {
            if (movement == Vector2.zero)
            {
                renderer.sprite = sprites[0];
            }
            else
            {
                int random = Random.Range(0, 2);
                renderer.sprite = sprites[(int)ANIMATIONSTATE.MOVE_01 + random];
            }
        }

        m_time += Time.deltaTime;

    }

    private void FixedUpdate()
    {
        ProceedMovement();
        ProceedRotation();
    }

    public void ProceedRotation()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        rigidbody.angularVelocity = 0.0f;
    }

    public void ProceedMovement() {
        movement.x = 0;
        movement.y = 0;
        if(Input.GetKey(KeyCode.A))
        {
            movement.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y -= 1;
        }

        if (Input.GetKey(KeyCode.Space) == false)
        {
            rigidbody.velocity = movement.normalized * movementSpeed;
        }
        else
        {
            rigidbody.velocity = movement.normalized * movementSpeed * boostFaktor;
        }
    }

    public void Dead()
    {
        DontDestroyOnLoadClass.SecondsAlive = (int)seconds;
        SceneManager.LoadScene(sceneName: "endscreen");
    }

    public virtual bool wouldCollide(Bounds bounds)
    {
        Vector3 size = m_collider.bounds.size;
        size.x += playerSafeAreaRadius;
        size.y += playerSafeAreaRadius;
        size.z = 1;
        Bounds biggerPlayerBounds = new Bounds(m_collider.bounds.center, size);
        return biggerPlayerBounds.Intersects(bounds);
    }

    public void attackMe(int damage)
    {
        m_health -= damage;
        if(m_health<=0)
        {
            Dead();
        }
    }

    private void shootProjectile()
    {
        if (m_time >= m_shootCooldown)
        {
            m_shootSound.Play();
            Vector3 direction = this.transform.up.normalized;
            var projectilePos = new Vector2(transform.position.x, transform.position.y) + new Vector2(direction.x * transform.localScale.x / 2, direction.y * transform.localScale.y / 2);
            Projectile.createNewProjectile(projectilePos, new Vector2(1.2f, 1.2f), this.transform.rotation, new Vector2(direction.x, direction.y), 45f, true, 1, 1);
            m_time = 0;
        }
    }

}