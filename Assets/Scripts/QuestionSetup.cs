// This script controls choosing, presenting and randomizing questions & answers
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DentedPixel;
using UnityEngine.UIElements;

public class QuestionSetup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float remainingTime;
    [SerializeField] public float remainingTimeDifficult;
    public GameObject timeBar;
    public int requestedTime;
    public int difficultQuestionReqTime;
    [SerializeField]
    public List<QuestionData> questions;
    public QuestionData currentQuestion;

    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private TextMeshProUGUI categoryText;
    [SerializeField]
    private AnswerButton[] answerButtons;

    [SerializeField]
    public int correctAnswerChoice;
    public int correctAnswerCount = 0;
    public int wrongAnswerCount = 0;
    public TextMeshProUGUI correctAnswerNumber;
    public TextMeshProUGUI wrongAnswerNumber;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI questionNumberText;
    public int questionNumber = 0;
    public int score;
    private int highscore;
    public TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject mainGamePanel;
    QuizPauseController quizPause;
    // private int highscore;
    // public TextMeshProUGUI highscoreText;

    private JokerScripts jokerScripts;
    private void Awake()
    {
        // Get all the questions ready
        GetQuestionAssets();
        score = 0;
        
    }

    // Start is called before the first frame update
    public void Start()
    {
        jokerScripts = GameObject.FindWithTag("JokerManager").GetComponent<JokerScripts>();
            //Get a new question
        SelectNewQuestion();
        // Set all text and values on screen
        SetQuestionValues();
        // Set all of the answer buttons text and correct answer values
        SetAnswerValues();
        
        LoadHighScore();
    }
    

        // if (PlayerPrefs.HasKey("highscore"))
        // {
        //     highscore = PlayerPrefs.GetInt("highscore");
        // }
        // else
        //     highscore = 0;
    
    private void GetQuestionAssets()
    {
        // Get all of the questions from the questions folder
        questions = new List<QuestionData>(Resources.LoadAll<QuestionData>("Questions"));
    }
    // private void Update() {
    //     StartCountdown();
    //     correctAnswerNumber.text = "Doğru: " + correctAnswerCount.ToString();
    //     wrongAnswerNumber.text = "Yanlış: "+ wrongAnswerCount.ToString();
    //     isFinished();
    //     highscoreText.text = "REKOR: "+ highscore.ToString();
    //     if(questionNumber == 21)
    //     {
    //         Debug.Log("Highscore is saved.");
    //         SaveHighScore();
    //     }
    // }
    public void LoadHighScore()
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            highscore = PlayerPrefs.GetInt("highscore");
            Debug.Log("it has highscore");
        }
        else    
            highscore = 0;
    }
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("highscore");
        PlayerPrefs.Save();
        highscore = 0;
    }
    private void FixedUpdate()
    {
        StartCountdown();
        correctAnswerNumber.text = "Doğru: " + correctAnswerCount.ToString();
        wrongAnswerNumber.text = "Yanlış: " + wrongAnswerCount.ToString();
        isFinished();
        highscoreText.text = "REKOR: " + highscore.ToString();
        if (questionNumber == 21)
        {
            SaveHighScore();
        }
    }
    public void SaveHighScore()
    {
        if (score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);
            Debug.Log("highscore is " + highscore.ToString());
        }
    }

    public void SelectNewQuestion()
    {
        // Get a random value for which question to choose
        int randomQuestionIndex = Random.Range(0, questions.Count);
        //Set the question to the randon index
        currentQuestion = questions[randomQuestionIndex];
        // Remove this questionm from the list so it will not be repeared (until the game is restarted)
        questions.RemoveAt(randomQuestionIndex);
        remainingTime = 30.2f;
        remainingTimeDifficult = 45.2f;
        requestedTime = 29;
        difficultQuestionReqTime = 44;
        questionNumber++;
        questionNumberText.text = questionNumber.ToString() + "/20";
        AnimateBar();
        StartCountdown();
        SetQuestionValues();
        SetAnswerValues();
    }

    private void SetQuestionValues()
    {
        // Set the question text
        questionText.text = currentQuestion.question;
        // Set the category text
        categoryText.text = currentQuestion.category; 
    }

    private void SetAnswerValues()
    {
        // Randomize the answer button order
        List<string> answers = RandomizeAnswers(new List<string>(currentQuestion.answers));
        // Set up the answer buttons
        for (int i = 0; i < answerButtons.Length; i++)
        {
            jokerScripts.Buttons[i].SetActive(true);
            // Create a temporary boolean to pass to the buttons
            bool isCorrect = false;
            // If it is the correct answer, set the bool to true
            if (i == correctAnswerChoice)
            {
                isCorrect = true;
            }
            answerButtons[i].SetIsCorrect(isCorrect);
            answerButtons[i].SetAnswerText(answers[i]);
        }
    }
    private List<string> RandomizeAnswers(List<string> originalList)
    {
        bool correctAnswerChosen = false;

        List<string> newList = new List<string>();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Get a random number of the remaining choices
            int random = Random.Range(0, originalList.Count);

            // If the random number is 0, this is the correct answer, MAKE SURE THIS IS ONLY USED ONCE
            if (random == 0 && !correctAnswerChosen)
            {
                correctAnswerChoice = i;
                correctAnswerChosen = true;
            }
            // Add this to the new list
            newList.Add(originalList[random]);
            //Remove this choice from the original list (it has been used)
            originalList.RemoveAt(random);
        }
        return newList;
    }

    public void AnimateBar()
    {
        if (currentQuestion.category == "ORTA" || currentQuestion.category == "KOLAY")
        {
            //Debug.Log("kolay ya da orta timebar");
            LeanTween.cancel(timeBar);
            timeBar.transform.localScale = new Vector3(1f, timeBar.transform.localScale.y, timeBar.transform.localScale.z);
            LeanTween.scaleX(timeBar, 0, requestedTime); // x teki boyutunu requestedTime süresince küçültüyor.
            // LeanTween.pause(timeBar);
        }
        else if (currentQuestion.category == "ZOR")
        {
            LeanTween.cancel(timeBar);
            timeBar.transform.localScale = new Vector3(1f, timeBar.transform.localScale.y, timeBar.transform.localScale.z);
            LeanTween.scaleX(timeBar, 0, difficultQuestionReqTime); // x teki boyutunu requestedTime süresince küçültüyor.
        }


    }
    public void StartCountdown()
    {
        if (currentQuestion.category == "KOLAY" || currentQuestion.category == "ORTA")
        {
            if (remainingTime > 0f)
            {
                remainingTime -= Time.deltaTime;

            }
            else
            {
                remainingTime = 0f;
                if (score >= 20)
                    score -= 20;
                scoreText.text = score.ToString();
                SelectNewQuestion();
            }
            int secondsRT = Mathf.FloorToInt(remainingTime % 60); // float olan remainingTime 60 ile mod alıp tavana yuvarlanıp seconds eşitleniyor.

            timerText.text = string.Format("{0:00}", secondsRT); // text olarak saniyeyi yazıyoruz.
        }
        else if (currentQuestion.category == "ZOR")
        {
            if (remainingTimeDifficult > 0f)
            {
                remainingTimeDifficult -= Time.deltaTime;

            }
            else
            {
                remainingTimeDifficult = 0f;
                if (score >= 20)
                    score -= 20;
                scoreText.text = score.ToString();
                SelectNewQuestion();
            }
            int seconds = Mathf.FloorToInt(remainingTimeDifficult % 60); // float olan remainingTime 60 ile mod alıp tavana yuvarlanıp seconds eşitleniyor.

            timerText.text = string.Format("{0:00}", seconds); // text olarak saniyeyi yazıyoruz.
        }
    }
    public void isFinished()
    {

        if (questionNumber == 21)
        {
            questionNumberText.text = "20/20";
            finalScoreText.text = "Toplam Skor: " + score;
            endGamePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1;
    }

    // public void AnimateBar()
    // {
    //     if(currentQuestion.category == "ORTA" || currentQuestion.category == "KOLAY")
    //     {
    //         // Debug.Log("kolay ya da orta timebar");
    //         LeanTween.cancel(timeBar);
    //         timeBar.transform.localScale = new Vector3(1f, timeBar.transform.localScale.y, timeBar.transform.localScale.z);
    //         LeanTween.scaleX(timeBar, 0, requestedTime); // x teki boyutunu requestedTime süresince küçültüyor.
    //         // LeanTween.pause(timeBar);
    //     }
    //     else if (currentQuestion.category == "ZOR")
    //     {
    //         // Debug.Log("zor timebar");
    //         LeanTween.cancel(timeBar);
    //         timeBar.transform.localScale = new Vector3(1f, timeBar.transform.localScale.y, timeBar.transform.localScale.z);
    //         LeanTween.scaleX(timeBar, 0, difficultQuestionReqTime); // x teki boyutunu requestedTime süresince küçültüyor.
    //         // LeanTween.pause(timeBar);
    //     }
        
        
    // }
    // public void StartCountdown() 
    // {
    //     if(currentQuestion.category ==  "KOLAY" || currentQuestion.category == "ORTA")
    //     {
    //         // Debug.Log("kolay ya da orta time");
    //         if (remainingTime > 0f )
    //         {
    //             remainingTime -= Time.deltaTime; 
                    
    //         }
    //         else
    //         {
    //             remainingTime = 0f;
    //             if(score >= 20)
    //                 score -= 20;
    //             else
    //                 score = 0;
    //             Debug.Log("zaman tükkkkkk");
    //             scoreText.text = score.ToString();
    //             SelectNewQuestion();
    //         }
    //         int secondsRT = Mathf.FloorToInt(remainingTime % 60); // float olan remainingTime 60 ile mod alıp tavana yuvarlanıp seconds eşitleniyor.
        
    //         timerText.text = string.Format("{0:00}", secondsRT); // text olarak saniyeyi yazıyoruz.
    //     }
    //     else if (currentQuestion.category == "ZOR")
    //     {
    //         // Debug.Log("zor time");
    //         if (remainingTimeDifficult > 0f )
    //         {
    //             remainingTimeDifficult -= Time.deltaTime; 
                    
    //         }
    //         else
    //         {
    //             remainingTimeDifficult = 0f;
    //             if(score >= 20 )
    //                 score -= 20;
    //             else
    //                 score = 0;
    //             Debug.Log("zaman tükkkkkk");
    //             scoreText.text = score.ToString();
    //             SelectNewQuestion();
    //         }
    //         int seconds = Mathf.FloorToInt(remainingTimeDifficult % 60); // float olan remainingTime 60 ile mod alıp tavana yuvarlanıp seconds eşitleniyor.
        
    //         timerText.text = string.Format("{0:00}", seconds); // text olarak saniyeyi yazıyoruz.
    //     }
        
    // }
    // public void isFinished()
    // {
        
    //     if(questionNumber == 21)
    //     {
    //         questionNumberText.text = "20/20";
    //         finalScoreText.text = "Toplam Skor: " + score;
    //         endGamePanel.SetActive(true);
    //         Time.timeScale = 0;
    //     }
    //     else 
    //         Time.timeScale = 1;
    // }
}