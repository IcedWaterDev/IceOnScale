using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timerText;
    private float time;

    private TimeSpan timespan;

    [SerializeField]
    private ScaleManager scaleManager;
    private string timeScore;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        timespan = TimeSpan.FromSeconds(time);
        
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timespan.Hours, timespan.Minutes, timespan.Seconds);

        /*if (scaleManager.isGameOver == true)
        {
            timeScore = timerText.text;
            Debug.Log("Game Over, Score = "+ timeScore);
        }*/
    }
}
