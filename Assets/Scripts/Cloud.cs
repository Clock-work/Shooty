using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField]
    public float speed = 2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if(transform.position.x >= 80)
        {
            transform.Translate(new Vector3(transform.position.x - 130 * 2, 0, 0));
        }
    }
}
