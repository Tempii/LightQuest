using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] UnityEngine.UI.Image start;
    [SerializeField] UnityEngine.UI.Image quit;
    // Start is called before the first frame update

    bool startSelected;
    void Start()
    {
        startSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            startSelected = !startSelected;
            print("Switched to: " + startSelected);
            if (startSelected)
            {
                start.color = new Color32(255, 214, 97, 255);
                quit.color = new Color(255, 255, 255, 255);
            }
            else
            {
                start.color = new Color32(255, 255, 255, 255);
                quit.color = new Color32(255, 214, 97, 255);
            }
        }

        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Submit"))
        {
            if (startSelected)
            {
                SceneManager.LoadScene("Level01");
            }
            else
            {
                Application.Quit();
            }
        }

    }
}
