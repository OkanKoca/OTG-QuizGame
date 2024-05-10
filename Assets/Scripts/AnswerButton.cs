// This script is for the buttons the answers will go on

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class AnswerButton : MonoBehaviour
{
    private bool isCorrect;
    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button button;

    // To make it ask a new question after the first question
    [SerializeField] private QuestionSetup questionSetup;
    public Color startColor;
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;

    private ColorBlock cb;

    public AudioSource correctSound;
    public AudioSource wrongSound;

    private void Start()
    {
        cb = button.colors;
    }
    private void Update()
    {
        if (isCorrect)
        {
            cb.pressedColor = correctColor;
        }
        else
        {
            cb.pressedColor = wrongColor;
        }
    }

    public void SetAnswerText(string newText)
    {
        answerText.text = newText;
    }

    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }

    public void OnClick()
    {
        if(isCorrect)
        {
            cb.pressedColor = correctColor;
            Debug.Log("CORRECT ANSWER");
            questionSetup.correctAnswerCount += 1;
            if(questionSetup.currentQuestion.category == "KOLAY") // kolay soru +50 puan
            {
                Debug.Log("kolay");
                questionSetup.score += 50;
            }
            else if (questionSetup.currentQuestion.category == "ORTA") // orta soru +75 puan
            {
                Debug.Log("orta");
                questionSetup.score += 75;
            }
            else if(questionSetup.currentQuestion.category == "ZOR") // zor soru +100 puan
            {
                Debug.Log("zor");
                questionSetup.score += 100;
            }
            scoreText.text = questionSetup.score.ToString();
            correctSound.Play();

        }
        else
        {
            cb = button.colors;
            cb.normalColor = startColor;
            cb.pressedColor = wrongColor;
            Debug.Log("WRONG ANSWER");
            questionSetup.wrongAnswerCount += 1;
            if(questionSetup.currentQuestion.category == "KOLAY" || questionSetup.currentQuestion.category == "ORTA" || questionSetup.currentQuestion.category == "ZOR") // yanlış sorularda 40 puan eksiliyor.
            {
                Debug.Log("kolay");
                if(questionSetup.score >= 40 )
                    questionSetup.score -= 40;
                else
                    questionSetup.score = 0;
            }
            scoreText.text = questionSetup.score.ToString();
            wrongSound.Play();
        }

        // Get the next question if there are more in the list
        if (questionSetup.questions.Count > 0)
        {
            // Generate a new question
            questionSetup.Start();
        }
    }
}