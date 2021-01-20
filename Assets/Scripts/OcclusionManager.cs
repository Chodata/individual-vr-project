using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionManager : MonoBehaviour
{
    [SerializeField]
    private MyInterface myInterface;
    [SerializeField]
    private MyObject myObject;
    [SerializeField]
    private Camera camera;
    private bool isAtLeft = false, isAtRight = false;
    private RaycastHit hit;
    [SerializeField]
    private GameObject rotator;
    private float rotateAngle = 20f, scaleAmount, scalePercent = 0.5f;
    private int rotateSpeed = 2;
    private bool isRotated = false, isRotating = false;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool doRotate = !isRotating && isBlocked();
        if(doRotate){
            if(isAtLeft){
                StartCoroutine(RotateInterface("RIGHT"));
                StartCoroutine(ScaleInterface(true));
                // StartCoroutine(RotateInterface("RIGHT"));
                isAtLeft = false;
                // isAtRight = true;

            } else if (isAtRight){
                StartCoroutine(RotateInterface("LEFT"));
                StartCoroutine(ScaleInterface(true));
                // StartCoroutine(RotateInterface("LEFT"));
                isAtRight = false;
                // isAtLeft = true;
            } else{
                if(hit.point.x >= myInterface.transform.position.x){
                    isAtLeft = true;
                    StartCoroutine(RotateInterface("LEFT"));
                } else{
                    isAtRight = true;
                    StartCoroutine(RotateInterface("RIGHT"));
                }
                StartCoroutine(ScaleInterface(false));
            }
        }

    }
    IEnumerator ScaleInterface(bool enlarge){
        int iter = (int)rotateAngle/rotateSpeed;
        Vector3 originScale = myInterface.transform.localScale;
        if(enlarge){
            scaleAmount = (originScale.x / scalePercent) - originScale.x;
            Debug.Log("Englarge");
        } else{
            scaleAmount = originScale.x * scalePercent;
            Debug.Log("delarge");

        }

        for(int i = 0; i < iter; i++){
            if(enlarge){
                myInterface.transform.localScale += new Vector3(scaleAmount / iter,0,0);
            } else{
                myInterface.transform.localScale -= new Vector3(scaleAmount / iter,0,0);
            }
            yield return new WaitForEndOfFrame();
        }
        if(enlarge){
            originScale.x += scaleAmount;
            myInterface.transform.localScale = originScale;
        } else{
            originScale.x -= scaleAmount;
            myInterface.transform.localScale = originScale;
        }
    }

    IEnumerator RotateInterface(string direction){
        isRotating = true;
        float angle;
        int counter = 0;
        do{
            if(direction == "RIGHT"){
                rotator.transform.Rotate(0f, rotateSpeed,0f, Space.Self);

            } else if(direction == "LEFT"){
                rotator.transform.Rotate(0f, -rotateSpeed,0f, Space.Self);
            }
            yield return new WaitForEndOfFrame();
            counter += rotateSpeed;
            // angle = Mathf.Clamp(rotator.transform.rotation.eulerAngles.y, -rotateAngle, rotateAngle);
            // rotator.transform.rotation.eulerAngles = Quaternion.Euler(0.0f, angle, 0.0f); 
            // Debug.Log(rotator.transform.rotation.y);
        // }while(Mathf.Abs(rotator.transform.rotation.eulerAngles.y) < rotateAngle);
        }while(rotateAngle > counter);
        isRotating = false;

    }



    public bool isBlocked(){
        Vector3 startRay = myObject.transform.position;
        Vector3 endRay = myObject.transform.TransformDirection(camera.transform.position - myObject.transform.position);
        if (Physics.Raycast(startRay, endRay, out hit, 100) && hit.collider.gameObject.tag == "MyInterface")
        {
            Debug.DrawRay(startRay, endRay * hit.distance, Color.red);
            // Debug.Log("Did Hit");
            return true;
        }
        else
        {
            Debug.DrawRay(startRay, endRay * 1000, Color.white);
            // Debug.Log("Did not Hit");
            return false;
        }
    }
}
