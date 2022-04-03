using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WakeUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] TextMeshProUGUI text3;
    [SerializeField] TextMeshProUGUI text4;
    [SerializeField] TextMeshProUGUI text5;
    // Start is called before the first frame update

    bool allShown;
    void Start()
    {
        StartCoroutine(WriteDialogue());
        allShown = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("Jump"))
        {
            if(allShown)
            {
                SceneManager.LoadScene("Menu");
            }

            text1.enabled = true;
            text2.enabled = true;
            text3.enabled = true;
            text4.enabled = true;
            text5.enabled = true;
            allShown = true;
        }

    }

    IEnumerator WriteDialogue()
    {
        text1.enabled = true;

        yield return new WaitForSeconds(3);


        text2.enabled = true;

        yield return new WaitForSeconds(3);


        text3.enabled = true;

        yield return new WaitForSeconds(7);


        text4.enabled = true;

        yield return new WaitForSeconds(8);


        text5.enabled = true;
        allShown = true;

        yield return new WaitForSeconds(11);

        SceneManager.LoadScene("Menu");
    }
}
