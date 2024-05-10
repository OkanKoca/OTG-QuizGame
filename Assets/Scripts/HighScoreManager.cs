using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    private int highscore;
    public TextMeshProUGUI highscoreText;
    QuestionSetup questionSetup;
    // Start is called before the first frame update
    void Start()
    {
        questionSetup = FindObjectOfType<QuestionSetup>();
        if(questionSetup == null)
    {
        Debug.Log("QuestionSetup component not found!");
    }
    else
    {
        LoadHighScore();
    }
        // LoadHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        highscoreText.text = "REKOR: "+ highscore.ToString();
        if(questionSetup.questionNumber == 20)
        {
            Debug.Log("Highscore is saved.");
            SaveHighScore();
        }
    }
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
    public void SaveHighScore()
    {
        if (questionSetup.score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", questionSetup.score);
            Debug.Log("highscore is " + highscore.ToString());
        }
    }
}
