using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class TimeBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject timeBar;
    public int requestedTime;
    
    void Start()
    {
        AnimateBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AnimateBar()
    {
        LeanTween.scaleX(timeBar, 0, requestedTime); // x teki boyutunu requestedTime süresince küçültüyor.
    }
}
