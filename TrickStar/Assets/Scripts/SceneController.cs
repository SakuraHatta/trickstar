using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private bool sceneMove = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SceneControl(){
        if (sceneMove){
            if (Input.GetKeyDown(KeyCode.Return)){
                // TittleからMainにシーン移動
                if (SceneManager.GetActiveScene().name == "TittleScene"){
                    SceneManager.LoadScene("MapScene");
                }
                // EndからTittleにシーン移動
                if (SceneManager.GetActiveScene().name == "EndScene"){
                    SceneManager.LoadScene("EndScene");
                }
            }
        }
    }
}
