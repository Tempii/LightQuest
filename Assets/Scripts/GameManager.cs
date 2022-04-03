using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float gameTimer = 120f;
    [SerializeField] GameObject timerText;
    [SerializeField] bool hideCursor;
    [SerializeField] Camera mainCamera;


    float currentTimer;
    bool gameRunning;

    PostProcessVolume volume;
    PostProcessProfile profile;
    Vignette vignette;
    ColorGrading grading;

    float postX = 1f;
    float postY = 0.8f;
    float postZ = 0f;

    void Start()
    {
        volume = mainCamera.GetComponent<PostProcessVolume>();
        profile = volume.profile;

        vignette = profile.GetSetting<Vignette>();
        grading = profile.GetSetting<ColorGrading>();

        currentTimer = gameTimer;
        gameRunning = true;
        if (hideCursor)
        {
            Cursor.visible = false;
        }

        /*vignette = ScriptableObject.CreateInstance<Vignette>();
        vignette.enabled.Override(true);
        vignette.intensity.Override(1f);*/
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

        PostProcessing();
    }

    private void PostProcessing()
    {
        float postProcessingFactor = currentTimer / gameTimer;
        if(postProcessingFactor > 1)
        {
            postProcessingFactor = 1;
        }
        print(currentTimer + " / " + gameTimer);

        print("Factor: " + postProcessingFactor);

        postProcessingFactor = 1f - postProcessingFactor;

        vignette.intensity.value = postProcessingFactor;
        grading.lift.value = new Vector4(postX, postY, postZ, 1f) * postProcessingFactor;
        grading.gamma.value = new Vector4(postX, postY, postZ, 1f) * postProcessingFactor;
        grading.gain.value = new Vector4(postX, postY, postZ, 1f) * postProcessingFactor;
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
