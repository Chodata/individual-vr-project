using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayHandler : Handler
{
    [SerializeField]
    private GameObject pivot;
    [SerializeField]
    private GameObject cameraOffSet;
    // Start is called before the first frame update
    void Start()
    {
        pivot.transform.position = base.camera.transform.position;
        pivot.transform.rotation = base.camera.transform.rotation;

        myInterface.transform.position = new Vector3(
            base.camera.transform.position.x + 0.4f,
            base.camera.transform.position.y,
            base.camera.transform.position.z + 2.5f
            );
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(pivot.transform.position);
        Debug.Log(cameraOffSet.transform.position);
        pivot.transform.position = base.camera.transform.position;
        pivot.transform.rotation = base.camera.transform.rotation;
    }



    public override void HandleTechnique(){
        return;
    }


}
