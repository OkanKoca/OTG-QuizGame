using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class TimeCountDown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    QuestionSetup questionSetup;
    TimeBarScript timeBarScript;
    
    // Start is called before the first frame update
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        
        // StartCountdown();

    }
    public void StartCountdown() // belki her soruda fonksiyonu kullanarak başlatırız diye ayrı bir fonk yaptım.
    {
        if (remainingTime >= 1f )
        {
            remainingTime -= Time.deltaTime; 
        }
        else if (remainingTime <= 0.5f) // remaningTime sıfıra geldiğinde duruyor.
            remainingTime = 0;
        
        int seconds = Mathf.FloorToInt(remainingTime % 60); // float olan remainingTime 60 ile mod alıp tavana yuvarlanıp seconds eşitleniyor.
        
        timerText.text = string.Format("{0:00}", seconds); // text olarak saniyeyi yazıyoruz.
    }
    public float GetRemainingTime()
    {
        return remainingTime;
    }
    
}
