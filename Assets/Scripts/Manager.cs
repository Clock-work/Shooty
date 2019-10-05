using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance = null;

    private GameObject m_leftBounds, m_rightBounds, m_topBounds, m_botBounds;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        var cameraMinPos = DefaultCamera.instance.getMinPos();
        var cameraMaxPos = DefaultCamera.instance.getMaxPos();
        createBounds(ref m_leftBounds, cameraMinPos.x, 0, 1, cameraMaxPos.y * 2);
        createBounds(ref m_rightBounds, cameraMaxPos.x, 0, 1, cameraMaxPos.y * 2);
        createBounds(ref m_topBounds, 0, cameraMinPos.y, cameraMaxPos.x * 2, 1);
        createBounds(ref m_botBounds, 0, cameraMaxPos.y, cameraMaxPos.x * 2, 1);

        for (int i = 0;i<50;++i)
        {
            DefaultEnemy.createRandomEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createBounds(ref GameObject direction, float x, float y, float width, float height)
    {
        var gameobject = Instantiate((GameObject)Resources.Load("Bounds"), new Vector3(x, y, 0), Quaternion.identity);
        gameobject.transform.localScale = new Vector3(width, height, 1);
        direction = gameobject;
    }

}
