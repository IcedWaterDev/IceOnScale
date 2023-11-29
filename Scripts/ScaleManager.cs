using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class ScaleManager : MonoBehaviour
{
    public float weightOnScale;
    //public int score = 0; //untuk testing leaderboard

    private float playerWeight;
    [SerializeField]
    private float maxWeightOnScale;
    [SerializeField]
    private float delayTimeForScale;
    [SerializeField]
    private float gameOverDelayTime;

    public GameObject player;
    public GameObject icePlane;
    public GameObject spawner;

    [SerializeField]
    private TMP_Text weightText;
    [SerializeField]
    private TMP_Text maxWeightText;

    [SerializeField]
    private string gameSceneName;

    private bool isScaleBroken;
    private bool isGameOver;

    //Time
    [SerializeField]
    private TMP_Text timerText;
    private TimeSpan timespan;
    public string timeScore;
    private float time;

    //Time old style version
    [SerializeField]
    private TMP_Text timerTextOldStyle;
    public float timeOld;

    //leaderboard
    [SerializeField]
    private GameObject Leaderboard;
    [SerializeField]
    private LeaderboardManager leaderboardManager;
    [SerializeField]
    private TMP_Text scoreText;

    private void Start()
    {
        isScaleBroken = false;
        isGameOver = false;
        playerWeight = player.GetComponent<Rigidbody2D>().mass;
        weightOnScale = playerWeight;
        maxWeightText.text = maxWeightOnScale.ToString();
    }

    private void Update()
    {
        ChangeWeightText();

        Stopwatch();

        if (weightOnScale > maxWeightOnScale && isScaleBroken == false)
        {
            ScaleBroken(); //Broke the scale and isGameOver = true
            StartCoroutine(NewGameCoroutine());
        }

        if (isGameOver == true)
        {
            SaveTimestamp();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            weightOnScale -= collision.attachedRigidbody.mass;
        }
    }

    private void ChangeWeightText()
    {
        weightText.text = weightOnScale.ToString();
    }

    private void ScaleBroken()
    {
        icePlane.SetActive(false);
        isGameOver = true;
        isScaleBroken = !isScaleBroken;
        spawner.SetActive(false);
    }

    private void SaveTimestamp()
    {
        for (int i = 0; i < 1; i++)
        {
            timeScore = timerText.text;
            Debug.Log("Game Over, Score = " + timeScore);
            Debug.Log("Best score = " + timeOld);
            leaderboardManager._playerScore = ((int)timeOld);
            scoreText.text = timeOld.ToString();
        }
        isGameOver = false;
    }

    IEnumerator NewGameCoroutine()
    {
        yield return new WaitForSeconds(gameOverDelayTime);
        //SceneManager.LoadScene(gameSceneName);
        //Time.timeScale = 0;
        Leaderboard.SetActive(true);
    }

    private void Stopwatch()
    {
        //Stopwatch Start
        time += Time.deltaTime;

        timespan = TimeSpan.FromSeconds(time);

        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timespan.Hours, timespan.Minutes, timespan.Seconds);

        //Stopwatch End

        timeOld = timespan.Seconds;
        timerTextOldStyle.text = timeOld.ToString();
    }
}
