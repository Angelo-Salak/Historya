using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelSelection : MonoBehaviour
{
    public Button[] buttons;
    int levelsUnlocked;
    // public Button lvlButton1;
    // public Button lvlButton2;
    // public Button lvlButton3;
    // public Button lvlButton4;
    // public Button lvlButton5;
    // public Button lvlButton6;
    // public Button lvlButton7;
    // public Button lvlButton8;
    // public Button lvlButton9;
    // public Button lvlButton10;
    
    
    public void ResetLevels ()
    {
        PlayerPrefs.DeleteAll();
    }


    public void UnlockAllLevels()
    {
        PlayerPrefs.SetInt("levelsUnlocked",  11);
    }

    

    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 2);

        for (int i = 0; i < buttons.Length; i++)
        {       
            if (i +2 > levelsUnlocked)
            {
                buttons[i].interactable = false;
            }
        }



//-----------------------------------------------------------------------------------------------------------------


        // lvlButton2.interactable = false;
        // lvlButton3.interactable = false;
        // lvlButton4.interactable = false;
        // lvlButton5.interactable = false;
        // lvlButton6.interactable = false;
        // lvlButton7.interactable = false;
        // lvlButton8.interactable = false;
        // lvlButton9.interactable = false;
        // lvlButton10.interactable = false;


//-----------------------------------------------------------------------------------------------------------------
        // switch (stringlevelPassed)
        // {
        //     case ("UnlockLevel2"):
        //         lvlButton2.interactable = true;
        //         break;
        //     case "UnlockLevel3":
        //         lvlButton2.interactable = true;
        //         lvlButton3.interactable = true;
        //         break;
        //     case "UnlockLevel4":
        //         lvlButton2.interactable = true;
        //         lvlButton3.interactable = true;
        //         lvlButton4.interactable = true;
        //         break;
        //     case "UnlockLevel5":
        //         lvlButton2.interactable = true;
        //         lvlButton3.interactable = true;
        //         lvlButton4.interactable = true;
        //         lvlButton5.interactable = true;
        //         break;
        //     case "UnlockLevel6":
        //         lvlButton2.interactable = true;
        //         lvlButton3.interactable = true;
        //         lvlButton4.interactable = true;
        //         lvlButton5.interactable = true;
        //         lvlButton6.interactable = true;
        //         break;

        // }

//-----------------------------------------------------------------------------------------------------------------
        
    }

   

    
}
