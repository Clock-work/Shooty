using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float Time = 30f;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float movementSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Object.Destroy(transform.gameObject, Time);
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.up * movementSpeed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {

    }
}
