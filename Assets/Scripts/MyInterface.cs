using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInterface : MonoBehaviour
{


    void Awake(){
        
    }

    void Start()
    {
    }

    public void setAngle(Quaternion newAngle){
        transform.rotation = newAngle;

    }


}
