using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField]
    public int Clouds = 10;

    [SerializeField]
    public float maxSpeed = 2f;

    [SerializeField]
    public float minSpeed = 1f;

    [SerializeField]
    public Vector3 leftSide = new Vector3(-50f, 0, 3f);

    [SerializeField]
    public GameObject[] clouds;

    private int CloudCount;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CloudCount = GameObject.FindGameObjectsWithTag("Cloud").Length;
        /**for(int i = 0; i < Clouds - CloudCount; i++)
        {
            float offset = Random.Range(-30, 30);
            int x = Random.Range(0, 7);
            int y = Random.Range(0, 35);
            GameObject cloud = Instantiate(clouds[x], new Vector3(leftSide.x + offset, leftSide.y + y, leftSide.z), Quaternion.identity);
            Cloud temp = cloud.GetComponent<Cloud>();
            temp.speed = Random.Range(minSpeed, maxSpeed);
        }**/

        for (int i = 0; i < Clouds - CloudCount; i++)
        {
            float scaleX = Random.Range(25, 40);
            float scaleY = Random.Range(25, 40);
            float offset = Random.Range(-70, 70);
            int x = Random.Range(0, 7);
            int y = Random.Range(-30, 0);
            GameObject cloud = Instantiate(clouds[i % Clouds], new Vector3(leftSide.x + offset, leftSide.y + y, leftSide.z), Quaternion.identity);
            cloud.transform.localScale = new Vector3(cloud.transform.localScale.x + scaleX, cloud.transform.localScale.y + scaleY, cloud.transform.localScale.z);
            Cloud temp = cloud.GetComponent<Cloud>();
            temp.speed = Random.Range(minSpeed * ((scaleX + scaleY) / 32), maxSpeed * ((scaleX + scaleY) / 32));
        }
    }
}
