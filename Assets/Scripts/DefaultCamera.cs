using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCamera : MonoBehaviour
{
    public static DefaultCamera instance = null;
    private Camera m_camera;
    public Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    public Vector2 getMinPos()
    {
        return m_camera.ScreenToWorldPoint(new Vector2(0, 0));
    }

    public Vector2 getMaxPos()
    {
        return m_camera.ScreenToWorldPoint(new Vector2(m_camera.pixelWidth, m_camera.pixelHeight));
    }

    private void Awake()
    {
        instance = this;
        this.m_camera = this.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
