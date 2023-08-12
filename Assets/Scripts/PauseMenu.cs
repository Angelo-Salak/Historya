using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
   public GameObject PBG;
   public GameObject pauseMenuPanel;

    public void pauseMenuMethod ()
    
    {
        PBG.SetActive(true);
        pauseMenuPanel.SetActive(true);
        
    }

    public void unpauseMenu()
    {
        PBG.SetActive(false);
        pauseMenuPanel.SetActive(false);
        
    }
}
