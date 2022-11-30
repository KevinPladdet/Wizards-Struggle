using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    private float startTime;
    private bool Finished = false;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Finished)
            return;

        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        float secI = (t % 60);
        string seconds = "";
        if (secI < 10)
        {
            seconds = "0";
        }
        seconds += (t % 60).ToString("f3");
        timerText.text = minutes + ":" + seconds;   
    }

    public void Finish()
    {
        Finished = true;
        timerText.color = Color.yellow;
    }
}
