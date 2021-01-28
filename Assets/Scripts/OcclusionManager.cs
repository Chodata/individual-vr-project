using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionManager : MonoBehaviour
{

    [SerializeField]
    private Handler techniqueHandler;

    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    

    void Update()
    {
        techniqueHandler.HandleTechnique();

    }
    


}
