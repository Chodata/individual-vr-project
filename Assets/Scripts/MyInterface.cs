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
    }


}
