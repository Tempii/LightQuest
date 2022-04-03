using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float gameTimer = 120f;
    [SerializeField] GameObject timerText;
    [SerializeField] bool hideCursor;
    [SerializeField] Camera mainCamera;

    [SerializeField] Canvas canvas;
    [SerializeField] UnityEngine.UI.Image cont;
    [SerializeField] UnityEngine.UI.Image quit;


    float currentTimer;
    bool gameRunning;

    bool continueSelected;
    bool paused;

    PostProcessVolume volume;

    public void Win()
    {
        SceneManager.LoadScene("Win");
    }

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

        continueSelected = true;
        paused = false;

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
            // Application.Quit();
        }

        UpdateTimer();

        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            print("P");
            paused = !paused;

            if (paused)
            {
                canvas.enabled = true;
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                canvas.enabled = false;
            }
        }

        if (paused)
        {
            MenuLogic();
        }

    }

    private void MenuLogic()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            continueSelected = !continueSelected;
            print("Switched to: " + continueSelected);
            if (continueSelected)
            {
                cont.color = new Color32(255, 214, 97, 255);
                quit.color = new Color(255, 255, 255, 255);
            }
            else
            {
                cont.color = new Color32(255, 255, 255, 255);
                quit.color = new Color32(255, 214, 97, 255);
            }
        }

        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Submit"))
        {
            if (continueSelected)
            {
                Time.timeScale = 1;
                canvas.enabled = false;
            }
            else
            {
                Application.Quit();
            }
        }
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
        // print(currentTimer + " / " + gameTimer);

        // print("Factor: " + postProcessingFactor);

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
        SceneManager.LoadScene("WakeUp");
    }
}
