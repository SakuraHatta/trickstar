using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public void OnClick(){
        Application.Quit();
        Debug.Log("click");
    }
}
