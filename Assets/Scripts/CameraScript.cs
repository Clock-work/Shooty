using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        prefab = (GameObject)Resources.Load("BuildCube");
    }

    // Update is called once per frame
    void Update()
    {
        var camera = this.GetComponent<Camera>();
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit1) && Input.GetMouseButtonDown(0) )
        {
            Instantiate(prefab, hit1.point, Quaternion.identity);
        }

        if (Physics.Raycast(ray, out RaycastHit hit2) && Input.GetMouseButtonDown(1))
        {
            var gameObject = hit2.transform.gameObject;
            if(gameObject.tag.Equals("build"))
            {
                Destroy(gameObject);
            }
        }

    }


}
