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

    private float seconds = 0;

    public Text cooldownText;

    public static PlayerScript instance;

    private Collider2D m_collider;

    private const float playerSafeAreaRadius = 20;

    private AudioSource m_shootSound;


    private int m_health;
    private int m_maxHealth;

    private float m_shootCooldown;
    private float m_time = 0;

    private int m_damage;
    private int m_charges;

    private bool m_shieldActive = false;
    private float m_shieldDuration;
    private float m_shieldCooldown;
    private float m_lastShieldTime = 0;

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
        movementSpeed = 5.0f;
        m_shootCooldown = 0.5f;
        m_maxHealth = 3;
        m_health = m_maxHealth;
        m_damage = 1;
        m_charges = 1;
        m_shieldDuration = 2;
        m_shieldCooldown = 30;
    }

    public void changeShootCooldown(float multiplier)
    {
        m_shootCooldown *= multiplier;
    }

    public void changeMovementSpeed(float multiplier)
    {
        movementSpeed *= multiplier;
    }

    public void changeHealth(int added)
    {
        m_health += added;
        m_maxHealth += added;
    }

    public void changeDamage(int added)
    {
        m_damage += added;
    }

    public void changePierce(int added)
    {
        m_charges += added;
    }

    public void upgradeSpecialAttacks(float positiveMultiplier)
    {
        float negativeMultiplier = 1 - (positiveMultiplier - 1);


        if(m_shieldCooldown > m_shieldDuration + 5)
        {
            m_shieldCooldown *= negativeMultiplier;
        }


    }

    // Update is called once per frame
    void Update()
    {
        Health.text = "HP: " + m_health.ToString();

        seconds += Time.deltaTime;

        if(Input.GetKey(KeyCode.Space) == true || Input.GetKey(KeyCode.Mouse0))
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

        if (Input.GetKey(KeyCode.C))
        {
            activateShield();
        }

        float cooldownLeft = m_lastShieldTime - seconds;
        if(cooldownLeft>0)
        {
            cooldownText.text = "shield cd: " + (int)cooldownLeft;
        }
        else
        {
            cooldownText.text = "shield cd: 0";
        }

        if(m_shieldActive && m_lastShieldTime - m_shieldCooldown + m_shieldDuration < seconds )
        {
            renderer.color = new Color(1, 1, 1, 1);
            m_shieldActive = false;
        }


    }

    private void FixedUpdate()
    {
        ProceedMovement();
        ProceedRotation();
    }

    private void activateShield()
    {
        if (m_lastShieldTime < seconds)
        {
            m_lastShieldTime = seconds + m_shieldCooldown;
            Color oldColor = renderer.color;
            if (!oldColor.Equals(new Color(2, 0, 0)))
            {
                m_shieldActive = true;
                renderer.color = new Color(0, 2, 0);
            }
        }
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
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x -= 1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement.x += 1;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement.y += 1;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement.y -= 1;
        }

        if (false && Input.GetKey(KeyCode.LeftShift) == false)//temp boost disabled
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
        if(m_shieldActive)
        {
            return;
        }
        m_health -= damage;
        if(m_health<=0)
        {
            Dead();
        }
        else
        {
            var color = renderer.color;
            renderer.color = new Color(2, 0, 0);
            StartCoroutine(timedColourSet(0.3f, color));
        }
    }

    private void shootProjectile()
    {
        if (m_time >= m_shootCooldown)
        {
            m_shootSound.Play();
            Vector3 direction = this.transform.up.normalized;
            var projectilePos = new Vector2(transform.position.x, transform.position.y) + new Vector2(direction.x * transform.localScale.x / 2, direction.y * transform.localScale.y / 2);
            Projectile.createNewProjectile(projectilePos, new Vector2(1.2f, 1.2f), this.transform.rotation, new Vector2(direction.x, direction.y), 45f, true, m_damage, m_charges);
            m_time = 0;
        }
    }

    private IEnumerator timedColourSet(float duration, Color color)
    {
        yield return new WaitForSeconds(duration);
        renderer.color = color;
    }

    public int getSecondsAlive()
    {
        return (int)seconds;
    }

}