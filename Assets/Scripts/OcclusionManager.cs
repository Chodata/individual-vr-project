using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionManager : MonoBehaviour
{
    public MyInterface myInterface;
    public MyObject myObject;
    public Camera camera;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 startRay = myObject.transform.position;
        Vector3 endRay = myObject.transform.TransformDirection(camera.transform.position - myObject.transform.position);
        if (Physics.Raycast(startRay, endRay, out hit, 100) && hit.collider.gameObject.tag == "MyInterface")
        {
            Debug.DrawRay(startRay, endRay * hit.distance, Color.red);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(startRay, endRay  * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
