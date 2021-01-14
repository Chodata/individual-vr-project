using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionManager : MonoBehaviour
{
    public MyInterface myInterface;
    public MyObject myObject;
    public Camera camera;
    public float animationTime = 0.25f;
    public bool isPlayingAnim = false;
    public bool isAtLeft = false, isAtRight = false;
    public RaycastHit hit;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool playAnim = isBlocked() && !isPlayingAnim;
        if(playAnim){
            isPlayingAnim = true;
            if(isAtLeft){
                StartCoroutine(PlayAnim());
                myInterface.resizeAnimator.SetTrigger("Left_Return");
            }
            else if (isAtRight){
                StartCoroutine(PlayAnim());
                myInterface.resizeAnimator.SetTrigger("Right_Return");

            } else if(playAnim){
                StartCoroutine(PlayAnim());
            }
        }

    }
    IEnumerator PlayAnim(){
        if(isAtLeft){ //initial pos at left
            yield return new WaitForSeconds(animationTime);
            myInterface.resizeAnimator.SetTrigger("Right");
            isAtLeft = false;
            isAtRight = true;
        } else if(isAtRight){ //initial pos at right
            yield return new WaitForSeconds(animationTime);
            myInterface.resizeAnimator.SetTrigger("Left");
            isAtLeft = true;
            isAtRight = false;
        } else{ //initial pos at center
            if(hit.point.x >= transform.position.x){
                isAtLeft = true;
                myInterface.resizeAnimator.SetTrigger("Left");
            } else{
                isAtRight = true;
                myInterface.resizeAnimator.SetTrigger("Right"); 
            }
        }
        yield return new WaitForSeconds(animationTime);
        isPlayingAnim = false;
    }



    public bool isBlocked(){
        Vector3 startRay = myObject.transform.position;
        Vector3 endRay = myObject.transform.TransformDirection(camera.transform.position - myObject.transform.position);
        if (Physics.Raycast(startRay, endRay, out hit, 100) && hit.collider.gameObject.tag == "MyInterface")
        {
            Debug.DrawRay(startRay, endRay * hit.distance, Color.red);
            Debug.Log("Did Hit");
            return true;
        }
        else
        {
            Debug.DrawRay(startRay, endRay * 1000, Color.white);
            Debug.Log("Did not Hit");
            return false;
        }
    }
}
