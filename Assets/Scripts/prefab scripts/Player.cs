using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Player : MonoBehaviour
{
    public static int health = 60;
     int maxHealth = 60;
    public GameObject deathEffect;
    public quizManager quizManage;
    public float DelayTime = 1f;
    public TextMeshProUGUI HealthTxt;
    public static int keepHealth;
    
    
    
    void Start()
    {
        
        //String healthIntToStr = health.ToString();
        //HealthTxt.text = health.ToString();
        var pos = new Vector3(150, Screen.height/2, 10);
        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }
    private void Update()
    {
        HealthTxt.text = health.ToString();
        if (health <= 0 ) 
        {
            Instantiate (deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            quizManage.GameOver();
            //StartCoroutine(WaitForSceneLoad());
            //SceneManager.LoadScene("Game Over");
             
        }

        if ( health >= maxHealth )
        {
            health = maxHealth;
        }
    }

    public void TakeDamage (int damage)
    {
        health -= damage;
        StartCoroutine(VisualIndicator(Color.red));
    }

    private IEnumerator WaitForSceneLoad() 
    {
     yield return new WaitForSeconds(1);
     SceneManager.LoadScene("Game Over");
     
    }

    private IEnumerator VisualIndicator (Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }
   
  
}
