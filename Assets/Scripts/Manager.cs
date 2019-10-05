using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance = null;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        var cameraMinPos = DefaultCamera.instance.getMinPos();
        var cameraMaxPos = DefaultCamera.instance.getMaxPos();

        for(int i = 0;i<50;++i)
        {
            float width = Random.Range(0.5f, 2f);
            float height = Random.Range(0.5f, 2f);
            float x = Random.Range(cameraMinPos.x + width, cameraMaxPos.x - width);
            float y = Random.Range(cameraMinPos.y + height, cameraMaxPos.y - height);
            float speed = Random.Range(0.5f, 5f);
            float targetX = Random.Range(-2, 2);
            float targetY = Random.Range(-2, 2);

            DefaultEnemy.createNewEnemy(x, y, width, height, targetX, targetY, speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
