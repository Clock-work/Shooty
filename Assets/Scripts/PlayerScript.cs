using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Shoot shootscript;

    [SerializeField]
    public Sprite[] sprites;

    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    private float seconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        shootscript = this.gameObject.GetComponent<Shoot>();
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = false;
        rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        ProceedMovement();
        ProceedRotation();

        if(Input.GetMouseButton(1) == true)
        {
            Dead();
        }

        if(Input.GetMouseButton(0) == true)
        {
            shootscript.ShotProjectile();
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
    }

    public void ProceedRotation()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public void ProceedMovement() {
        movement.x = Input.GetAxis("Horizontal") * movementSpeed;
        movement.y = Input.GetAxis("Vertical") * movementSpeed;

        if (Input.GetKey(KeyCode.Space) == false)
        {
            rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * boostFaktor * Time.fixedDeltaTime);
        }
    }

    public void Dead()
    {
        DontDestroyOnLoadClass.SecondsAlive = (int)seconds;
        SceneManager.LoadScene(sceneName: "endscreen");
    }
}