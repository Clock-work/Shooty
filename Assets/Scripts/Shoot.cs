using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField]
    public GameObject Projectile;

    [SerializeField]
    public float cooldown = 1f;

    [SerializeField]
    public float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        Projectile = (GameObject)Resources.Load("Prefabs/Projectile");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public void ShotProjectile()
    {
        if(time >= cooldown)
        {
            Transform projectile = Instantiate(Projectile.transform, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10f) + Vector3.forward * 10f, Quaternion.identity);
            projectile.rotation = transform.rotation;
            time = 0;
        }
        
    }
}
