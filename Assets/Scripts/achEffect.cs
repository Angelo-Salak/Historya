using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class achEffect : MonoBehaviour
{
    [SerializeField]private GameObject acheff;

    
    public void achEffects ()
    {
        //Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f,10));
        Instantiate(acheff, transform.right, Quaternion.identity);

        
    }
}
