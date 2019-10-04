using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LsdCubeScript : MonoBehaviour
{
    int lsdcounter = 0;
    int colourCounter = 0;
    public float moveSpeed = 8f;
    public float turnSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {

    }

    Vector3 getTranslationDirection()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        return direction;
    }

    // Update is called once per frame
    void Update()
    {
        if (lsdcounter++ >= 10)
        {
            lsdcounter = 0;

            if (++colourCounter > 3)
            {
                colourCounter = 1;
            }

            if (colourCounter == 1)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            if (colourCounter == 2)
            {
                GetComponent<Renderer>().material.color = Color.green;
            }
            if (colourCounter == 3)
            {
                GetComponent<Renderer>().material.color = Color.blue;
            }

        }

        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

        this.transform.Translate(getTranslationDirection() * moveSpeed * Time.deltaTime);



    }
}
