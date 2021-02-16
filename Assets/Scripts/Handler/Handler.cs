using System.Collections;
using System.Collections.Generic;
using UnityEngine;
abstract public class Handler: MonoBehaviour{
    [SerializeField]
    protected MyInterface myInterface;
    [SerializeField]
    protected GameObject myObject;
    [SerializeField]
    protected Camera camera;
    protected RaycastHit hit;

    public abstract void HandleTechnique();

    public bool isBlocked(){
    Vector3 startRay = myObject.transform.position;
    Vector3 endRay = myObject.transform.TransformDirection(camera.transform.position - myObject.transform.position);
    if (Physics.Raycast(startRay, endRay, out hit, 100) && hit.collider.gameObject.tag == "MyInterface")
    {
        Debug.DrawRay(startRay, endRay * hit.distance, Color.red);
        // Debug.Log("Did Hit");
        return true;
    }
    else {
        Debug.DrawRay(startRay, endRay * 1000, Color.white);
        // Debug.Log("Did not Hit");
        return false;
    }}

}
