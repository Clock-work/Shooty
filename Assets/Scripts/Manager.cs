using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance = null;

    private GameObject m_leftBounds, m_rightBounds, m_topBounds, m_botBounds;

    private float m_minBoundsSize;
    private float m_maxBoundsSize;
    private float m_currentBoundsSize;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        var cameraMinPos = DefaultCamera.instance.getMinPos();
        var cameraMaxPos = DefaultCamera.instance.getMaxPos();
        m_minBoundsSize = 6;
        m_maxBoundsSize = 40;
        m_currentBoundsSize = m_minBoundsSize;
        createBounds(ref m_leftBounds, cameraMinPos.x -2, 0, m_minBoundsSize, cameraMaxPos.y * 2);
        createBounds(ref m_rightBounds, cameraMaxPos.x +2, 0, m_minBoundsSize, cameraMaxPos.y * 2);
        createBounds(ref m_topBounds, 0, cameraMinPos.y -2, cameraMaxPos.x * 2, m_minBoundsSize);
        createBounds(ref m_botBounds, 0, cameraMaxPos.y +2, cameraMaxPos.x * 2, m_minBoundsSize);

        for (int i = 0;i<20;++i)
        {
            DefaultEnemy.createRandomEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            changeBoundSize(1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            changeBoundSize(-1);
        }
    }

    private void createBounds(ref GameObject direction, float x, float y, float width, float height)
    {
        var gameobject = Instantiate((GameObject)Resources.Load("Prefabs/Bounds"), new Vector3(x, y, 0), Quaternion.identity);
        gameobject.transform.localScale = new Vector3(width, height, 1);
        direction = gameobject;
    }

    //negative to decrease size for the bounds
    public void changeBoundSize(float addedSize)
    {
        m_currentBoundsSize += addedSize;
        if(m_currentBoundsSize<m_minBoundsSize)
        {
            m_currentBoundsSize = m_minBoundsSize;
        }
        if(m_currentBoundsSize>m_maxBoundsSize)
        {
            m_currentBoundsSize = m_maxBoundsSize;
        }
        m_leftBounds.transform.localScale = new Vector3(m_currentBoundsSize, m_leftBounds.transform.localScale.y, 1);
        m_rightBounds.transform.localScale = new Vector3(m_currentBoundsSize, m_rightBounds.transform.localScale.y, 1);
        m_topBounds.transform.localScale = new Vector3(m_topBounds.transform.localScale.x, m_currentBoundsSize, 1);
        m_botBounds.transform.localScale = new Vector3(m_botBounds.transform.localScale.x, m_currentBoundsSize, 1);
    }

}
