using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GlobalVar : MonoBehaviour
{
    // Start is called before the first frame update
    private static GlobalVar ins;
    public static int numberOfJump = 0;
    [SerializeField]
    private Text jumpText;
    private void Awake() {
        if(ins == null){
            ins = this;
        } else{
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if(currentScene.name == "MainMenu"){
            
            jumpText.text = "Jump: " + GlobalVar.numberOfJump.ToString();
        }  
    }

    // Update is called once per frame
    void Update()
    {


    }
}
