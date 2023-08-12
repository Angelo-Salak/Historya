using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class achievement : MonoBehaviour
{
    public GameObject deathEffect;
    public Button blocker1;
    public Button blocker2;
    public Button blocker3;
    public Button blocker4;
    public Button blocker5;
    public TextMeshProUGUI replace1;
    public TextMeshProUGUI replace2;
    public TextMeshProUGUI replace3;
    public TextMeshProUGUI replace4;
    public TextMeshProUGUI replace5;
    
    int achUnlocked;
    // Start is called before the first frame update
    void Start()
    {
       blocker1.interactable = false;
       blocker2.interactable = false;
       blocker3.interactable = false;
       blocker4.interactable = false;
       blocker5.interactable = false;
        achUnlocked = PlayerPrefs.GetInt("achUnlocked", 2);
         if (achUnlocked >= 2)
            {
                blocker1.interactable = true;
                replace1.text = "BAGUHAN";
            }
        if (achUnlocked >= 4)
            {
                blocker2.interactable = true;
                replace2.text = "APRENTIS";
            }
        if (achUnlocked >= 7)
            {
                blocker3.interactable = true;
                replace3.text = "BIBLYOTEKARYO";
            }
        if (achUnlocked >= 10)
            {
                blocker4.interactable =true;
                replace4.text = "PANTAS";
            }
        if (achUnlocked >= 11)
            {
                blocker5.interactable = true;
                replace5.text = "POONG MANANALAYSAY";
            }
        

        
    }

 public void ResetAch ()
    {
        PlayerPrefs.DeleteAll();
    }
}
