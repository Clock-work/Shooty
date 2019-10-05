using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float Time = 30f;

    // Start is called before the first frame update
    void Start()
    {
        Object.Destroy(transform.gameObject, Time);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward);
    }
}
