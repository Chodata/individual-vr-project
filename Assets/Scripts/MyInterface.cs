using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInterface : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 originPos, leftPos, rightPos;
    public Animator resizeAnimator;

    void Awake(){
        resizeAnimator = GetComponent<Animator>();
    }

    void Start()
    {

        originPos = transform.position;
        leftPos = transform.position + new Vector3(-1.5f,0,0);
        rightPos = transform.position + new Vector3(1.5f,0,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShiftRight(){
        transform.localScale = transform.localScale/2;
        
    }

    void ShiftLeft(){

    }

    void CancelShift(){
        transform.localScale = transform.localScale * 2;

    }



}
