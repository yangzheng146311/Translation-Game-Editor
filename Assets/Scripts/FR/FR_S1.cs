﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FR_S1 : MonoBehaviour
{
    public GameObject inputframe;
    public GameObject title;


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

    public void ShowInputFrame()
    {

        inputframe.SetActive(true);
    }

    public void ReplaceTitle()
    {



        if(inputframe.activeSelf==true)
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {

                if (inputframe.GetComponent<InputField>().text == "The Myth of Theseus")
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
        inputframe.GetComponent<InputField>().text = "The Myth of Theseus";


    }

    public void LoadNextScene()
    {

        SceneManager.LoadScene("FR_S2");

    }

    public void EndGame()
    {

        Debug.Log("quit");
        Application.Quit();
    }


}
