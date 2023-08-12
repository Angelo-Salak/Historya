using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject deathEffect;
    public quizManager quizManage;
    public GameObject enemyRef;
    
   
    SpriteRenderer spriteRenderer;
    private UnityEngine.Object enemyRef1;
    
     
    void Start()
    {
        enemyRef1 = Resources.Load("Enemy1");
        var pos = new Vector3(Screen.width-300, Screen.height/2, 10);
        transform.position = Camera.main.ScreenToWorldPoint(pos);
        
    }

     void Respawn ()
    {
        gameObject.SetActive(true);
    }

    private void disableEnemy()
    {
        Instantiate (deathEffect, transform.position, Quaternion.identity); 
       // gameObject.SetActive(true);
        //spriteRenderer.enabled=false;
        Invoke(nameof(Respawn),0.50f);
    }
   
    private void Update()
    {
        
        if (health<=0)
        {
            gameObject.SetActive(false);
         disableEnemy();
            health+=2;
        }  
    }
    
    public void killEnemy()
    {
      
        Instantiate (deathEffect, transform.position, Quaternion.identity); 
        Destroy(gameObject);
        quizManage.moveToBoss_Panel_1();
        
    }

    public void TakeDamage (int damage)
    {
        health -= damage;
        //StartCoroutine(VisualIndicator(Color.magenta));
        Invoke("VisualIndicator(Color.magenta)",0.75f);
        //StartCoroutine(VisualIndicator2(Color.magenta));
    }


     private IEnumerator VisualIndicator2 (Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;

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
