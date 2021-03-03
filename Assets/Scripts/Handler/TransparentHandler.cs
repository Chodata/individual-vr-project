using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentHandler : Handler
{    

    [SerializeField]
    private GameObject pivot;
    [SerializeField]
    private GameObject transparentObj;
    [SerializeField]
    private Material transparentMat;
    private float maxT = 1.0f; 
    private float minT = 0.6f;
    private float changeT = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        // myRenderTexture.mipMapBias = -0;
        // transparentMat = transparentObj.GetComponent<Material>();
        pivot.transform.position = base.camera.transform.position;
        pivot.transform.rotation = base.camera.transform.rotation;

        myInterface.transform.position = new Vector3(
            base.camera.transform.position.x -0.1f,
            base.camera.transform.position.y,
            base.camera.transform.position.z + 2.5f
            );
    }

    // Update is called once per frame
    void Update()
    {
        pivot.transform.position = base.camera.transform.position;
        pivot.transform.rotation = base.camera.transform.rotation;
    }


    public override void HandleTechnique(){
        Color matColor = transparentMat.color;
        Debug.Log(matColor.a);

        if(base.isBlocked()){
            if(matColor.a >= minT){
                matColor.a -= changeT * Time.deltaTime;
            }
        } else {
                matColor.a += changeT * Time.deltaTime;
        }
        matColor.a = Mathf.Clamp(matColor.a, minT, maxT);

        transparentMat.color = matColor;


    }



}
