using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JokerScripts : MonoBehaviour
{
    private QuestionSetup questionSetup;
    private int pass;
    private int fifty;
    private int doubleAns;
    [SerializeField]
    private TextMeshProUGUI passText;
    [SerializeField]
    private TextMeshProUGUI fiftyText;
    [SerializeField]
    private TextMeshProUGUI doubleAnsText;
    public bool isClickedDouble;
    public bool isClickedFifty;
    public List<GameObject> Buttons;
    void Start()
    {
        questionSetup = GameObject.FindWithTag("Question").GetComponent<QuestionSetup>();
        pass = 2;
        fifty = 2;
        doubleAns = 1;
        isClickedDouble = false;
        isClickedFifty = false;
    }
    public void PassJoker()
    {
        if (pass != 0)
        {
            pass -= 1;
            questionSetup.SelectNewQuestion();
        }
        passText.text = pass.ToString();
    }
    public void FiftyJoker()
    {
        if(fifty != 0)
        {
            int random1 = Random.Range(0, 4);
            int random2 = Random.Range(0, 4);
            if (random1 != questionSetup.correctAnswerChoice && random2 != questionSetup.correctAnswerChoice && random1 != random2)
            {
                Buttons[random1].SetActive(false);
                Buttons[random2].SetActive(false);
                Debug.Log(random1);
                Debug.Log(random2);
                fifty -= 1;
                fiftyText.text = fifty.ToString();
            }
            else
                FiftyJoker();
        }
    }
    public void DoubleAnswerJoker()
    {
        if (doubleAns != 0)
        {
            isClickedDouble = true;
            doubleAns -= 1;
        }
        doubleAnsText.text = doubleAns.ToString();
    }
}
