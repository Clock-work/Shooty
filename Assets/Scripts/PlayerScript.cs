using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
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


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        shootscript = this.gameObject.GetComponent<Shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        ProceedMovement();
        ProceedRotation();

        if(Input.GetMouseButton(0) == true)
        {
            shootscript.ShotProjectile();
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
}