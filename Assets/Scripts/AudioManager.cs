using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
