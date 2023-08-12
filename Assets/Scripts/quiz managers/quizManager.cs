using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class quizManager : MonoBehaviour
{
    
//timer
   [SerializeField] private TextMeshProUGUI timeText;
   [SerializeField] private float duration, currentTime;

    //level unlocker variables
    public int currentLevel;
    [SerializeField]private string passCode;

   //quiz ui stuff
   public List <questionsAndAnswers> QnA;
   public GameObject[] options;
   public int currentQuestion;


   public GameObject projectile2;
   public Transform shotPoint2;
   
   public TextMeshProUGUI QuestionTxt;
   public TextMeshProUGUI ScoreTxt;
   public TextMeshProUGUI GOText;
   [SerializeField]private Image spriteField;

   //game over panel
   
   public GameObject QuizPanel;
   
   public GameObject GOPanel;
   [SerializeField]private GameObject goToNextLevelPanel;

   //public Player player;
   public Enemy enemy;
   
   
   //boss Panel
   
   public GameObject moveToBossPanel_1;
   public Boss boss;
   
   public GameObject Manager_1;
   
 

   //pause panel
   //public GameObject PBG;
   public GameObject pauseMenuPanel;
   
   //quiz array stuff
   public int totalQuestions = 0;
   public int score;

   //load unload
   //public static quizManager Instance;
   [SerializeField]private int sceneNum;
   [SerializeField]private int sceneNext;
   [SerializeField]private int sceneRetry;
   public GameObject afterEffect;

   //Audio Sources
   [SerializeField] private AudioSource GameOverSoundFX;
   [SerializeField] private AudioSource SuccessSoundFX;
   [SerializeField] private AudioSource WrongSoundFX;
   [SerializeField] private AudioSource CorrectSoundFX;

   //Question Counter
   public TextMeshProUGUI QCounter;
   

    IEnumerator TimeIEn()
    {
        while (currentTime >= 0)
        {
            timeText.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
            
            
            if (pauseMenuPanel.activeInHierarchy == true)
            {
                currentTime++;
            }
            

            
            
           
            
        }
        if (Player.health > 0 && QnA.Count > 0)
        {
            wrong();
            Instantiate(projectile2,shotPoint2.position,transform.rotation);
            currentTime = duration;
            StartCoroutine(TimeIEn());
        }
    }

     



    public async void LoadBoss(int sceneNum)
    {
        var scene = SceneManager.LoadSceneAsync(sceneNum);
        scene.allowSceneActivation = true;

    }

     public async void NextLevel(int sceneNum)
    {
         SceneManager.LoadScene(sceneNext);
         Player.health += 60;
    }

   


    private void Start()
     {
        //revisions: Timer
        currentTime = duration;
        timeText.text = currentTime.ToString();
        StartCoroutine(TimeIEn());

       // Debug.Log("{APPTIM_EVENT}:"+ "Normal Monster Quiz, START"); 
        if (afterEffect != null)
        { afterEffect.SetActive(false);}
        
        totalQuestions = QnA.Count; 
        Debug.Log("QNA Count: " + QnA.Count);
        
       
        GOPanel.SetActive(false);
        

        generateQuestion();
        
      //  PBG.SetActive(false);

        //pauseMenuPanel.SetActive(false);

        GOPanel.SetActive(false);

        
     
        moveToBossPanel_1.SetActive(false);
        
       
     }






   
    public void GameOver()
    {
        
            // GOBG.SetActive(true);
            GOPanel.SetActive(true);
            QuizPanel.SetActive(false);
            
            // int endText = totalQuestions - QnA.Count; 
            // GOText.text = score + "/" +endText;

       
            //int endText = totalQuestions - QnA.Count; 
            GOText.text = score + "/" +totalQuestions;
            GameOverSoundFX.Play();
            
        
      
    }
   

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Player.health += 60;
    }

    public void retryBackToNormalMonster()
    {
        SceneManager.LoadScene(sceneRetry);
        Player.health += 60;
    }



    public void correct()
    {
        CorrectSoundFX.Play();
        score += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
        currentTime = duration;
        

    }


     public void wrong()
    {
        
        WrongSoundFX.Play();
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
        currentTime = duration;
        
        
    }


    void SetAnswers()
    {

        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA [currentQuestion].Answers[i];
            
        

            if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }

    }


    private IEnumerator delayedKill (float time)
    {
        yield return new WaitForSeconds(time);
        enemy.killEnemy();
        
    }

   void generateQuestion()
   {
        if (QnA.Count > 0)
        {
            
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Question;
            if (spriteField != null){
            spriteField.sprite = QnA[currentQuestion].qSprite;}

            SetAnswers();
            Debug.Log("{APPTIM_EVENT}:"+ "NORMAL MONSTER QUIZ GENERATE QUESTION, START");  
            Debug.Log("{APPTIM_EVENT}:"+ "NORMAL MONSTER QUIZ GENERATE QUESTION , STOP");  
            Debug.Log("Questions Remaining: "+ QnA.Count);
            QCounter.text = QnA.Count.ToString();

             
        }
       
 

        else
        {
            if (Player.health > 0)
            {QCounter.text = "0";}
            
            Debug.Log("Out Of Questions");
           // Debug.Log("{APPTIM_EVENT}:"+ "Normal Monster Quiz, STOP");
            StartCoroutine(delayedKill(0.30f));
            if (afterEffect != null)
            {afterEffect.SetActive(true);}
            
            if (passCode == "yy")
                    {
                        if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
                        {
                            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
                        }
                        if (currentLevel >= PlayerPrefs.GetInt("achUnlocked"))
                        {
                            PlayerPrefs.SetInt("achUnlocked", currentLevel + 1);
                        }
                    }
            
                  
        }

   }


    public void moveToBoss_Panel_1()
    {
        SuccessSoundFX.Play();
        QuizPanel.SetActive(false);
        moveToBossPanel_1.SetActive(true);
        if (ScoreTxt != null)
        {
             int endText = totalQuestions - QnA.Count; 
             ScoreTxt.text = score + "/" +endText;
        }
    }


    public void moveToBoss()
    {
        Debug.Log("Total Questions: " + totalQuestions);
        Debug.Log("QNA Count: " + QnA.Count);
        Debug.Log("QNA Count: " + QnA);
        
        LoadBoss(sceneNum);
    }

    public void moveToNextLevel()
    {
        Debug.Log("Total Questions: " + totalQuestions);
        Debug.Log("QNA Count: " + QnA.Count);
        Debug.Log("QNA Count: " + QnA);
        
        NextLevel(sceneNext);
    }

}
