using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsManager : MonoBehaviour
{
    public static BoundsManager instance = null;

    private GameObject m_leftBounds, m_rightBounds, m_topBounds, m_botBounds;

    private float m_minBoundsSize;
    private float m_maxBoundsSize;
    private float m_currentBoundsWidth;
    private float m_currentBoundsHeight;

    public static Vector2 getInternalMinPos()
    {
        return new Vector2(instance.m_leftBounds.transform.position.x + instance.m_leftBounds.transform.localScale.x/2, instance.m_botBounds.transform.position.y + instance.m_botBounds.transform.localScale.y/2);
    }

    public static Vector2 getInternalMaxPos()
    {
        return new Vector2(instance.m_rightBounds.transform.position.x - instance.m_rightBounds.transform.localScale.x/2, instance.m_topBounds.transform.position.y - instance.m_topBounds.transform.localScale.y/2);
    }

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
        m_currentBoundsWidth = m_minBoundsSize;
        m_currentBoundsHeight = m_minBoundsSize;
        createBounds(ref m_leftBounds, cameraMinPos.x -2, 0, m_minBoundsSize, cameraMaxPos.y * 2);
        createBounds(ref m_rightBounds, cameraMaxPos.x +2, 0, m_minBoundsSize, cameraMaxPos.y * 2);
        createBounds(ref m_botBounds, 0, cameraMinPos.y -2, cameraMaxPos.x * 2, m_minBoundsSize);
        createBounds(ref m_topBounds, 0, cameraMaxPos.y +2, cameraMaxPos.x * 2, m_minBoundsSize);
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
        m_currentBoundsWidth += addedSize *1.8f;
        m_currentBoundsHeight += addedSize;
        if (m_currentBoundsWidth < m_minBoundsSize)
        {
            m_currentBoundsWidth = m_minBoundsSize;
        }
        if(m_currentBoundsWidth > m_maxBoundsSize * 1.8f)
        {
            m_currentBoundsWidth = m_maxBoundsSize * 1.8f;
        }
        if (m_currentBoundsHeight < m_minBoundsSize)
        {
            m_currentBoundsHeight = m_minBoundsSize;
        }
        if (m_currentBoundsHeight > m_maxBoundsSize)
        {
            m_currentBoundsHeight = m_maxBoundsSize;
        }
        m_leftBounds.transform.localScale = new Vector3(m_currentBoundsWidth, m_leftBounds.transform.localScale.y, 1);
        m_rightBounds.transform.localScale = new Vector3(m_currentBoundsWidth, m_rightBounds.transform.localScale.y, 1);
        m_topBounds.transform.localScale = new Vector3(m_topBounds.transform.localScale.x, m_currentBoundsHeight, 1);
        m_botBounds.transform.localScale = new Vector3(m_botBounds.transform.localScale.x, m_currentBoundsHeight, 1);
    }


}
