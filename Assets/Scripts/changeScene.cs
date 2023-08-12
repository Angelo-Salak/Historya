using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public void  moveToScene (int sceneID)
    {
        
        //SceneManager.LoadScene(sceneID);
        SceneManager.LoadScene(sceneID,LoadSceneMode.Single);
        Player.health += 60;
    }



}
