using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeHandler : Handler
{    
    [SerializeField]
    private GameObject rotator;
    private float rotateAngle = 14f, scaleAmount, scalePercent = 0.5f;
    private int rotateSpeed = 2;
    private bool isRotated = false, isRotating = false;
    private bool isAtLeft = false, isAtRight = false;
    [SerializeField]
    private GameObject cameraOffSet;
    [SerializeField]
    private GameObject pivot;
    private Quaternion previousAngle;

    // Start is called before the first frame update
    void Start()
    {
        pivot.transform.position = cameraOffSet.transform.position;
        pivot.transform.rotation = cameraOffSet.transform.rotation;
        previousAngle = cameraOffSet.transform.rotation;

        myInterface.transform.position = new Vector3(
            cameraOffSet.transform.position.x -0.1f,
            cameraOffSet.transform.position.y,
            cameraOffSet.transform.position.z + 2.5f
            );
        
        
    }

    // Update is called once per frame
    void Update()
    {
        pivot.transform.position = cameraOffSet.transform.position;
        pivot.transform.rotation = cameraOffSet.transform.rotation;

        // rotator.transform.position = cameraOffSet.transform.position;
        // // myInterface.setAngle(cameraOffSet.transform.rotation * rotator.transform.rotation);

        // rotator.transform.rotation = rotator.transform.rotation * (cameraOffSet.transform.rotation * Quaternion.Inverse(previousAngle)) ;
        // previousAngle = cameraOffSet.transform.rotation;


        
    }


    public override void HandleTechnique(){
        bool doRotate = !isRotating && base.isBlocked();
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
            scaleAmount = originScale.x / scalePercent;
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
        myInterface.transform.localScale = new Vector3(scaleAmount,originScale.y,originScale.z);

    }

    IEnumerator RotateInterface(string direction){
        isRotating = true;
        float angle;
        int counter = 0;
        do{
            yield return new WaitForEndOfFrame();

            if(direction == "RIGHT"){
                rotator.transform.Rotate(0f, rotateSpeed,0f, Space.Self);

            } else if(direction == "LEFT"){
                rotator.transform.Rotate(0f, -rotateSpeed,0f, Space.Self);
            }
            counter += rotateSpeed;
            // angle = Mathf.Clamp(rotator.transform.rotation.eulerAngles.y, -rotateAngle, rotateAngle);
            // rotator.transform.rotation.eulerAngles = Quaternion.Euler(0.0f, angle, 0.0f); 
            // Debug.Log(rotator.transform.rotation.y);
        // }while(Mathf.Abs(rotator.transform.rotation.eulerAngles.y) < rotateAngle);
        }while(rotateAngle > counter);
        yield return new WaitForSeconds(0.5f);
        isRotating = false;

    }


}
