using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health = 5;
    [SerializeField] TextMeshProUGUI counterTmpro;
    [SerializeField] TextMeshProUGUI counterTmpro2;
    float remainingTime = 900f;

    void Start()
    {
        HandleIdleTime();

        health = PlayerPrefs.GetInt("Health", health);
        float passedTime = GetPassedTime();
    }

    void Update()
    {

        if (health >= 5)
        {
            remainingTime = 900f;
        }
        else
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0f)
            {
                remainingTime = 900f;
                HealthIncrease();
            }
        }
        ShowCounterText();

    }

    private void ShowCounterText()
    {
        if (health >= 100)
        {
            counterTmpro.text = "";
            counterTmpro2.text = "";
        }
        else
        {
            float minutes = Mathf.FloorToInt(remainingTime / 60);
            float seconds = Mathf.FloorToInt(remainingTime % 60);
            string currentTime = string.Format("{00:00}:{1:00}", minutes, seconds);
            counterTmpro.text = "+50 Energy <#fff>in " + currentTime + " min";
            counterTmpro2.text = "+50 <#fff>in " + currentTime;
        }
    }
    public void HealthDecrease()
    {
        health--;
        PlayerPrefs.SetInt("Health", health);

    }
    public void HealthIncrease()
    {
        SaveDateTime();

        health++;

        health = Mathf.Clamp(health, 0, 5);
        if (health >= 5)
        {
            remainingTime = 900f;
            SaveDateTime();
        }
        PlayerPrefs.SetInt("Health", health);

    }

    private void HandleIdleTime()
    {
        float passedTime = GetPassedTime();
        if (passedTime >= 30f)
        {
            HealthIncrease();

        }

    }

    private float GetPassedTime()
    {
        float passedTime = -1f;
        DateTime timeNow = DateTime.UtcNow;
        if (PlayerPrefs.HasKey("HealthTimer"))
        {
            string timeAsString = PlayerPrefs.GetString("HealthTimer");
            DateTime clickLastDate = DateTime.Parse(timeAsString);
            passedTime = (float)(timeNow - clickLastDate).TotalMinutes;

        }
        else
        {
            passedTime = 31f;

        }

        return passedTime;
    }
    private void SaveDateTime()
    {
        PlayerPrefs.SetString("HealthTimer", DateTime.UtcNow.ToString());
    }

}
