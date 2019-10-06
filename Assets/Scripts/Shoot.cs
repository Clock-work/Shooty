using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    public AudioSource source;

    [SerializeField]
    public float cooldown;

    [SerializeField]
    public float time = 0;

    private void Awake()
    {
        source = this.gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

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
            source.Play();
            //Transform projectile = Instantiate(Projectile.transform, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10f) + Vector3.forward * 10f, Quaternion.identity);
            Vector3 direction = this.transform.up.normalized;
            var projectilePos = new Vector2(transform.position.x, transform.position.y) + new Vector2(direction.x * transform.localScale.x / 2, direction.y * transform.localScale.y / 2);
            Projectile.createNewProjectile(projectilePos, new Vector2(1.2f, 1.2f), this.transform.rotation, new Vector2(direction.x, direction.y), 45f, true, 1, 1);
            time = 0;
        }
        
    }
}
