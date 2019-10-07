using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    public float AnimationInterval;

    private SpriteRenderer spriteRenderer;

    private int indicie = 0;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
        time += Time.deltaTime;
    }

    virtual protected void Awake()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }


    public void Animate()
    {
        if (time > AnimationInterval)
        {
            spriteRenderer.sprite = sprites[indicie++ % sprites.Length];
            time = 0;
        }
        if(indicie>=sprites.Length)
        {
            Destroy(this.gameObject);
        }
    }

}
