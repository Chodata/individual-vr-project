using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MySceneManager : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void LoadSceneResize(){
          SceneManager.LoadScene("ResizeTech");
          GlobalVar.numberOfJump = 0;
     }
     public void LoadSceneMain(){
          SceneManager.LoadScene("MainMenu");
     }
     public void LoadSceneOverlay(){
          SceneManager.LoadScene("OverlayTech");
          GlobalVar.numberOfJump = 0;

     }
     public void LoadSceneTransparent(){
          SceneManager.LoadScene("TransparentTech");
          GlobalVar.numberOfJump = 0;

     }
}
