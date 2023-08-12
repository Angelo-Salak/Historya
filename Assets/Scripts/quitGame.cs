using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitGame : MonoBehaviour
{
    public void quitApp(){
        Application.Quit();
        Debug.Log("Quit!");
    }
}
