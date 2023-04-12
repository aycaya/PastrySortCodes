using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameTimer : MonoBehaviour
{
    float timeRemaining = 120;
    bool timerIsRunning = false;
    [SerializeField] TextMeshProUGUI timeText;
    ManageStar manageStar;
    void Start()
    {
        timerIsRunning = true;
        manageStar = GetComponent<ManageStar>();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                manageStar.DecreaseStar();

                timeRemaining = 0;
                timerIsRunning = false;
                Destroy(timeText);
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}