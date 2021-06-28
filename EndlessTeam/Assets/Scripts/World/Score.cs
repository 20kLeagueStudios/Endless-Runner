using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;

    int currentScore = 0;

    float timer = 0;

    float secondsToScore = 2f;

    private void Start()
    {
        timer = Time.time;
        scoreText.text = "Score: " + 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time - timer > secondsToScore)
        {
            TakeScore(1);
            timer = Time.time;
        }
    }

    public void TakeScore(int value)
    {
        currentScore += value;
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
