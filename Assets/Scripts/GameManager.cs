using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float gameTimer = 120f;
    [SerializeField] GameObject timerText;
    [SerializeField] bool hideCursor;

    float currentTimer;
    bool gameRunning;

    void Start()
    {
        currentTimer = gameTimer;
        gameRunning = true;
        if (hideCursor)
        {
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Cancel") > 0)
        {
            print("cancel");
            Application.Quit();
        }

        UpdateTimer();

    }

    private void UpdateTimer()
    {
        if (gameRunning && currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            if(currentTimer < 0)
            {
                currentTimer = 0;
                WakeUp();
            }
        }
        int minutes = (int)currentTimer / 60;
        int seconds = (int)currentTimer % 60;
        int millis = (int)((currentTimer % 1)*100);
        TextMeshProUGUI tmp = timerText.GetComponent<TextMeshProUGUI>();

        string secondsString;
        if (seconds < 10)
        {
            secondsString = "0" + seconds;
        }
        else
        {
            secondsString = "" + seconds;
        }

        string millisString;
        if (millis < 10)
        {
            millisString = "0" + millis;
        }
        else
        {
            millisString = "" + millis;
        }

        tmp.text = "<mspace=0.5em>" + minutes + ":" + secondsString + ":" + millisString + "</mspace>";

        /*if (currentTimer > 60)
        {
            tmp.text = "<mspace=0.5em>" + minutes + ":" + secondsString + ":" + millisString + "</mspace>";
        }
        else if ( currentTimer > 1)
        {
            tmp.text = "<mspace=0.5em>" + secondsString + ":" + millisString + "</mspace>";
        }
        else
        {
            tmp.text = "<mspace=0.5em>" + millisString + "</mspace>";
        }*/
    }

    public void AddTime(float timeBonus)
    {
        currentTimer += timeBonus;
    }

    private void WakeUp()
    {
        throw new NotImplementedException();
        
    }
}
