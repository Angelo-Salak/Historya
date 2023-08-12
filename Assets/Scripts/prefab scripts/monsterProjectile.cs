using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterProjectile : MonoBehaviour
{
     public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;
    public GameObject destroyEffect;

    public int damage;

    // Start is called before the first frame update
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    private void Update()
    {
        //var posiposi = ((transform.right*-1 )+(transform.up*-1));
        transform.Translate(transform.right*-1  * speed * Time.deltaTime);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right*-1,distance,whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                Debug.Log("PLAYER TOOK DAMAGE!");
                Debug.Log("{APPTIM_EVENT}:"+ "MONSTER PROJECTILE HIT, START");  
                    
                hitInfo.collider.GetComponent<Player>().TakeDamage(damage);
            }
            DestroyProjectile();
           
            Debug.Log("{APPTIM_EVENT}:"+ "MONSTER PROJECTILE HIT, STOP");  
        }


    }

    void DestroyProjectile()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
