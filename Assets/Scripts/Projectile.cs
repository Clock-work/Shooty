using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float Time = 30f;

    public Rigidbody2D rb;

    [SerializeField]
    private float movementSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Object.Destroy(transform.gameObject, Time);
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.Translate(Vector3.up * movementSpeed);
        Debug.Log("fly");
    }
}
