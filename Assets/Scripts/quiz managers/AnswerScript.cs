using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
   //weapon data stuff
    public GameObject projectile;
    public GameObject projectile2;
    public Transform shotPoint;
    public Transform shotPoint2;
    public float offset;
    
    
    //Answer script stuff
    public quizManager quizManage;
  
    
    public bool isCorrect = false;
  


    public void Answer()
    {
        if(isCorrect)
        {
           
            Debug.Log("Correct Answer");
            Debug.Log("QNA Count: " + quizManage.QnA.Count);
            Debug.Log("QNA Count: " + quizManage.totalQuestions);
            
            quizManage.correct();
         
            
            Instantiate(projectile,shotPoint.position,transform.rotation);
            Debug.Log("{APPTIM_EVENT}:"+ "PLAYER SHOOT, START");  
            Debug.Log("{APPTIM_EVENT}:"+ "PLAYER SHOOT, STOP"); 

        }

        else
        {
            
            Debug.Log("Wrong Answer");
            Debug.Log("QNA Count: " + quizManage.QnA.Count);
            Debug.Log("QNA Count: " + quizManage.totalQuestions);
            

            quizManage.wrong();
      
            
            Instantiate(projectile2,shotPoint2.position,transform.rotation);
            Debug.Log("{APPTIM_EVENT}:"+ "MONSTER SHOOT, START");  
            Debug.Log("{APPTIM_EVENT}:"+ "MONSTER SHOOT, STOP"); 
            
        }


    }

    
    


}
