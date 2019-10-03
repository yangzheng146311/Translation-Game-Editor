using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FR_END: MonoBehaviour
{
    public GameObject title;
    public GameObject inputframe;

    public GameObject correctResult;
    public GameObject wrongResult;

    static public string sourceTitle;
    static public string transTitle;


    // Start is called before the first frame update
    void Start()
    {
        sourceTitle = title.GetComponent<Text>().text;
    }

    // Update is called once per frame
    void Update()
    {
        ReplaceTitle();
    }

    public void ReplaceTitle()
    {



        if (inputframe.activeSelf == true)
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {

                if (inputframe.GetComponent<InputField>().text == "THE END")
                {



                    string inputtext = inputframe.GetComponent<InputField>().text;

                    title.GetComponent<Text>().text = inputtext;

                    transTitle = inputtext;


                    Debug.Log(transTitle);

                    correctResult.SetActive(true);

                }

                else
                {
                    wrongResult.SetActive(true);



                }



            }
        }
    }

    public void ShowCorrectAnswer()
    {
        inputframe.GetComponent<InputField>().text = "THE END";


    }

    public void EndGame()
    {

        Debug.Log("quit");
        Application.Quit();
    }
}
