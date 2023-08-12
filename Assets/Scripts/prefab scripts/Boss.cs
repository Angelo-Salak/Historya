using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    public int health;
    public GameObject deathEffect;
    public quizManager quizManage;
    
    public GameObject monsterRef;

    SpriteRenderer spriteRenderer;
    
    
     
    void Start()
    {
        
      gameObject.SetActive(false);
    }

     

   
    private void Update()
    {

        // if (health<=0)
        // {
        //  killEnemy();
        // }  

    }
    
    public void killEnemy()
    {
       
        Instantiate (deathEffect, transform.position, Quaternion.identity); 
        Destroy(gameObject);
        quizManage.GameOver();
        
    }

    public void TakeDamage (int damage)
    {
        health -= damage;
        //StartCoroutine(VisualIndicator(Color.magenta));
        Invoke("VisualIndicator(Color.magenta)",0.75f);
    }


   

    private void VisualIndicator (Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        //yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }



    // public void AutoKill ()
    // {
    //     health -= 99999;
    //     StartCoroutine(VisualIndicator(Color.magenta));
    // }


}
