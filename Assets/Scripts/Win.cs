using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField] GameObject scroll;
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] TextMeshProUGUI text3;
    [SerializeField] TextMeshProUGUI text4;
    [SerializeField] TextMeshProUGUI text5;
    [SerializeField] TextMeshProUGUI text6;
    [SerializeField] TextMeshProUGUI text7;

    bool allShown;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WriteDialogue());
        allShown = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (allShown)
            {
                SceneManager.LoadScene("Menu");
            }

            text1.enabled = false;
            scroll.GetComponent<SpriteRenderer>().enabled = false;
            text2.enabled = true;
            text3.enabled = true;
            text4.enabled = true;
            text5.enabled = true;
            text6.enabled = true;
            text7.enabled = true;
            allShown = true;
        }
    }

    IEnumerator WriteDialogue()
    {

        yield return new WaitForSeconds(7);

        text1.enabled = false;
        scroll.GetComponent<SpriteRenderer>().enabled = false;


        yield return new WaitForSeconds(1);

        text2.enabled = true;

        yield return new WaitForSeconds(3);

        text3.enabled = true;

        yield return new WaitForSeconds(3);


        text4.enabled = true;

        yield return new WaitForSeconds(7);


        text5.enabled = true;

        yield return new WaitForSeconds(9);


        text6.enabled = true;

        yield return new WaitForSeconds(2);

        text7.enabled = true;
        allShown = true;

        yield return new WaitForSeconds(9);

        SceneManager.LoadScene("Menu");
    }
}
