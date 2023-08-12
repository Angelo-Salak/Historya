using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class QuizLogoManager : MonoBehaviour
{
    //timer
   [SerializeField]private TextMeshProUGUI timeText;
   [SerializeField]private float duration, currentTime;
   [SerializeField]private GameObject OptionHolder;
   [SerializeField]private GameObject pauseMenuPanel;


    //level unlocker variables
    public int currentLevel;
    [SerializeField]private string passCode;
    

    //Attack Variables
    public GameObject projectile;
    public GameObject projectile2;
    public Transform shotPoint;
    public Transform shotPoint2;
    public float offset;
    public Enemy enemy;
    [SerializeField] private GameObject youDiedPanel;
    

   

    public static QuizLogoManager instance; //Instance to make is available in other scripts without reference

    [SerializeField] private GameObject gameComplete;
    //Scriptable data which store our questions data
    [SerializeField] private QuizDataScriptable questionDataScriptable;
    [SerializeField] private Image questionImage;           //image element to show the image
    [SerializeField] private WordData[] answerWordList;     //list of answers word in the game
    [SerializeField] private WordData[] optionsWordList;    //list of options word in the game


    private GameStatus gameStatus = GameStatus.Playing;     //to keep track of game status
    private char[] wordsArray = new char[18];               //array which store char of each options

    private List<int> selectedWordsIndex;                   //list which keep track of option word index w.r.t answer word index
    private int currentAnswerIndex = 0, currentQuestionIndex = 0;   //index to keep track of current answer and current question
    private bool correctAnswer = true;                      //bool to decide if answer is correct or not
    private string answerWord;                              //string to store answer of current question



//Sound FX
   [SerializeField] private AudioSource GameOverSoundFX;
   [SerializeField] private AudioSource SuccessSoundFX;
   [SerializeField] private AudioSource WrongSoundFX;
   [SerializeField] private AudioSource CorrectSoundFX;



    IEnumerator TimeIEn()
    {
        //revisions: Timer
        while (currentTime >= 0)
        {
            timeText.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
            
            if (pauseMenuPanel.activeInHierarchy == false)
            {
                continue;
            }
            if (pauseMenuPanel.activeInHierarchy == true)
            {
                currentTime++;
            }
            
            
            
        }
        
        if (Player.health > 0 && currentQuestionIndex < questionDataScriptable.questions.Count)
        {
            WrongSoundFX.Play();
            Instantiate(projectile2,shotPoint2.position,transform.rotation);
            currentTime = duration;
            
            

                ResetWord();   
                gameStatus = GameStatus.Next; //set the game status
                
                currentQuestionIndex++; //increase currentQuestionIndex 
                
                
                 
                if (currentQuestionIndex < questionDataScriptable.questions.Count)
                {
                    Invoke("SetQuestion", 0.4f); 
                    Debug.Log("{APPTIM_EVENT}:"+ "BOSS Monster LOGO GENERATE QUESTION, START");  
                    Debug.Log("{APPTIM_EVENT}:"+ "BOSS Monster LOGO GENERATE QUESTION, STOP"); 
                   
                }
                else
                {
                     
                    Debug.Log("Game Complete"); 
                    StartCoroutine(LogoGameOver(0.3f));
                    StartCoroutine(LogoGameOver2(0.5f));
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
             StartCoroutine(TimeIEn());
            // if (ResetWord())
            // {
            //     StartCoroutine(TimeIEn());
                
            // }
        }
    






    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedWordsIndex = new List<int>();           //create a new list at start
        SetQuestion();    
                                    //set question


        //revisions: Timer
        currentTime = duration;
        timeText.text = currentTime.ToString();
        StartCoroutine(TimeIEn());
    }


    void Update ()
    {
        if (Player.health == 0)
        {
            GameOverSoundFX.Play();
            youDiedPanel.SetActive(true);
        }
    }

  

    void SetQuestion()
    {
        gameStatus = GameStatus.Playing;                //set GameStatus to playing 
      
        //set the answerWord string variable
        answerWord = questionDataScriptable.questions[currentQuestionIndex].answer;
        //set the image of question
        questionImage.sprite = questionDataScriptable.questions[currentQuestionIndex].questionImage;
        
            
        ResetQuestion();                               //reset the answers and options value to orignal     

        selectedWordsIndex.Clear();                     //clear the list for new question
        Array.Clear(wordsArray, 0, wordsArray.Length);  //clear the array

        //add the correct char to the wordsArray
        for (int i = 0; i < answerWord.Length; i++)
        {
            wordsArray[i] = char.ToUpper(answerWord[i]);
        }

        //add the dummy char to wordsArray
        for (int j = answerWord.Length; j < wordsArray.Length; j++)
        {
            wordsArray[j] = (char)UnityEngine.Random.Range(65, 90);
        }

        wordsArray = ShuffleList.ShuffleListItems<char>(wordsArray.ToList()).ToArray(); //Randomly Shuffle the words array

        //set the options words Text value
        for (int k = 0; k < optionsWordList.Length; k++)
        {
            optionsWordList[k].SetChar(wordsArray[k]);
        }

    }

    //Method called on Reset Button click and on new question
    public void ResetQuestion()
    {
        //activate all the answerWordList gameobject and set their word to "_"
        for (int i = 0; i < answerWordList.Length; i++)
        {
            answerWordList[i].gameObject.SetActive(true);
            answerWordList[i].SetChar('_');
        }

        //Now deactivate the unwanted answerWordList gameobject (object more than answer string length)
        for (int i = answerWord.Length; i < answerWordList.Length; i++)
        {
            answerWordList[i].gameObject.SetActive(false);
        }

        //activate all the optionsWordList objects
        for (int i = 0; i < optionsWordList.Length; i++)
        {
            optionsWordList[i].gameObject.SetActive(true);
           
        }

        currentAnswerIndex = 0;
    }



    /// <summary>
    /// When we click on any options button this method is called
    /// </summary>
    /// <param name="value"></param>

    public void SelectedOption(WordData value)
    {
        //if gameStatus is next or currentAnswerIndex is more or equal to answerWord length
        if (gameStatus == GameStatus.Next || currentAnswerIndex >= answerWord.Length) return;

        selectedWordsIndex.Add(value.transform.GetSiblingIndex()); //add the child index to selectedWordsIndex list
        value.gameObject.SetActive(false); //deactivate options object
        answerWordList[currentAnswerIndex].SetChar(value.charValue); //set the answer word list

        currentAnswerIndex++;   //increase currentAnswerIndex

        //if currentAnswerIndex is equal to answerWord length
        if (currentAnswerIndex == answerWord.Length)
        {
            correctAnswer = true;   //default value
            //loop through answerWordList
            for (int i = 0; i < answerWord.Length; i++)
            {
                //if answerWord[i] is not same as answerWordList[i].wordValue
                if (char.ToUpper(answerWord[i]) != char.ToUpper(answerWordList[i].charValue))
                {
                    correctAnswer = false; //set it false
                    break; //and break from the loop
                }
            }

            //if correctAnswer is true
            if (correctAnswer)
            {
                currentTime = duration;
                CorrectSoundFX.Play();
                Debug.Log("Correct Answer");
                gameStatus = GameStatus.Next; //set the game status
                currentQuestionIndex++; //increase currentQuestionIndex
                Instantiate(projectile,shotPoint.position,transform.rotation); // Player Attack
                Debug.Log("{APPTIM_EVENT}:"+ "PLAYER SHOOT, START");  
                Debug.Log("{APPTIM_EVENT}:"+ "PLAYER SHOOT, STOP"); 
               
                

                //if currentQuestionIndex is less that total available questions
                if (currentQuestionIndex < questionDataScriptable.questions.Count)
                {
                    Invoke("SetQuestion", 0.4f); //go to next question
                    Debug.Log("{APPTIM_EVENT}:"+ "BOSS Monster LOGO GENERATE QUESTION, START");  
                    Debug.Log("{APPTIM_EVENT}:"+ "BOSS Monster LOGO GENERATE QUESTION, STOP"); 
                    
                }
                else
                {
                    SuccessSoundFX.Play();
                    Debug.Log("Game Complete"); //else game is complete
                   // gameComplete.SetActive(true);
                    StartCoroutine(LogoGameOver(0.3f));
                    StartCoroutine(LogoGameOver2(0.5f));
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

            else //if wrong answer
            {   
                WrongSoundFX.Play();
                currentTime = duration;
                Instantiate(projectile2,shotPoint2.position,transform.rotation); // Monster Attack Because of Wrong Answer
                Debug.Log("{APPTIM_EVENT}:"+ "MONSTER SHOOT, START");  
                Debug.Log("{APPTIM_EVENT}:"+ "MONSTER SHOOT, STOP"); 
                ResetWord();   
                gameStatus = GameStatus.Next; //set the game status
                
                currentQuestionIndex++; //increase currentQuestionIndex 
                
                
                 
                if (currentQuestionIndex < questionDataScriptable.questions.Count)
                {
                    Invoke("SetQuestion", 0.4f); 
                    Debug.Log("{APPTIM_EVENT}:"+ "BOSS Monster LOGO GENERATE QUESTION, START");  
                    Debug.Log("{APPTIM_EVENT}:"+ "BOSS Monster LOGO GENERATE QUESTION, STOP"); 
                   
                }
                else
                {
                    SuccessSoundFX.Play();
                    Debug.Log("Game Complete"); 
                    StartCoroutine(LogoGameOver(0.3f));
                    StartCoroutine(LogoGameOver2(0.5f));
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
        }
    }

    
    private IEnumerator LogoGameOver (float time)  //Delay Method for killing the Boss
    {
        yield return new WaitForSeconds(time);
        enemy.killEnemy();
        
    }
    private IEnumerator LogoGameOver2 (float time) //Delay method for activating Game OVer Screen
    {
        yield return new WaitForSeconds(time);
        gameComplete.SetActive(true);
        OptionHolder.SetActive(false);
        
    }

    public void ResetLastWord()                 //Removes last inputted letter/char
    {
        if (selectedWordsIndex.Count > 0)
        {
            int index = selectedWordsIndex[selectedWordsIndex.Count - 1];
            optionsWordList[index].gameObject.SetActive(true);
            selectedWordsIndex.RemoveAt(selectedWordsIndex.Count - 1);

            currentAnswerIndex--;
            answerWordList[currentAnswerIndex].SetChar('_');
        }
    }

    public void ResetWord()             //removes all letter
    {
        while (selectedWordsIndex.Count > 0)
        {
            int index = selectedWordsIndex[selectedWordsIndex.Count - 1];
            optionsWordList[index].gameObject.SetActive(true);
            selectedWordsIndex.RemoveAt(selectedWordsIndex.Count - 1);

            currentAnswerIndex--;
            answerWordList[currentAnswerIndex].SetChar('_');
        }
       // return true;
    }

}

[System.Serializable]
public class QuestionData
{
    public Sprite questionImage;
    public string answer;
}

public enum GameStatus
{
   Next,
   Playing
}


























