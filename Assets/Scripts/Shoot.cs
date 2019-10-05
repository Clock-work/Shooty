using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField]
    public GameObject Projectile;

    // Start is called before the first frame update
    void Start()
    {
        Projectile = (GameObject)Resources.Load("Projectile");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShotProjectile()
    {
        Transform projectile = Instantiate(Projectile.transform, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10f) + Vector3.forward * 10f, Quaternion.identity);
    }
}
