// This script controls choosing, presenting and randomizing questions & answers

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DentedPixel;

public class QuestionSetup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float remainingTime;
    public GameObject timeBar;
    public int requestedTime;
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
    private int correctAnswerChoice;
    public int correctAnswerCount = 0;
    public int wrongAnswerCount = 0;
    public TextMeshProUGUI correctAnswerNumber;
    public TextMeshProUGUI wrongAnswerNumber;
    [SerializeField] private TextMeshProUGUI scoreText;
    public int score;
    private bool isFailed = false;

    private void Awake()
    {
        // Get all the questions ready
        GetQuestionAssets();
        score = 0;
        
    }

    // Start is called before the first frame update
    public void Start()
    {
        //Get a new question
        SelectNewQuestion();
        // Set all text and values on screen
        SetQuestionValues();
        // Set all of the answer buttons text and correct answer values
        SetAnswerValues();
        
        
    }

    private void GetQuestionAssets()
    {
        // Get all of the questions from the questions folder
        questions = new List<QuestionData>(Resources.LoadAll<QuestionData>("Questions"));
    }
    private void Update() {
        StartCountdown();
        correctAnswerNumber.text = "Correct: " + correctAnswerCount.ToString();
        wrongAnswerNumber.text = "Wrong: "+ wrongAnswerCount.ToString();
    }

    public void SelectNewQuestion()
    {
        // Get a random value for which question to choose
        int randomQuestionIndex = Random.Range(0, questions.Count);
        //Set the question to the randon index
        currentQuestion = questions[randomQuestionIndex];
        // Remove this questionm from the list so it will not be repeared (until the game is restarted)
        questions.RemoveAt(randomQuestionIndex);
        remainingTime = 30.5f;
        requestedTime = 30;
        AnimateBar();
        StartCountdown();
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
            // Create a temporary boolean to pass to the buttons
            bool isCorrect = false;

            // If it is the correct answer, set the bool to true
            if(i == correctAnswerChoice)
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

        List<string>  newList = new List<string>();

        for(int i = 0; i < answerButtons.Length; i++)
        {
            // Get a random number of the remaining choices
            int random = Random.Range(0, originalList.Count);

            // If the random number is 0, this is the correct answer, MAKE SURE THIS IS ONLY USED ONCE
            if(random == 0 && !correctAnswerChosen)
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
        LeanTween.cancel(timeBar);
        timeBar.transform.localScale = new Vector3(1f, timeBar.transform.localScale.y, timeBar.transform.localScale.z);
        LeanTween.scaleX(timeBar, 0, requestedTime); // x teki boyutunu requestedTime süresince küçültüyor.
    }
    public void StartCountdown() 
    {
        if (remainingTime >= 1f )
        {
            remainingTime -= Time.deltaTime; 
        }
        else if (remainingTime <= 0.5f) // remaningTime sıfıra geldiğinde duruyor.
            remainingTime = 0;
        else if (remainingTime == 0)
            isFailed = true;
        
        
        int seconds = Mathf.FloorToInt(remainingTime % 60); // float olan remainingTime 60 ile mod alıp tavana yuvarlanıp seconds eşitleniyor.
        
        timerText.text = string.Format("{0:00}", seconds); // text olarak saniyeyi yazıyoruz.
    }
    public void isFail()
    {
        
        if(isFailed)
        {
            scoreText.text = "Total score: " + score;
        }
    }
}