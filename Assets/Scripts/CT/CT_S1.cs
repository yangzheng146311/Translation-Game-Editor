using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CT_S1: MonoBehaviour
{
    public GameObject yinyangObject;
    public GameObject rollText;
    public GameObject startPanel;
    public GameObject wrongPanel;
    public GameObject translationPanel;

    public AudioClip encourage;
    
    public static string sourceRollingText;
    public static string transRollingText;
    public static string sourceCourageText;
    public static string transCourageText;
    public static string sourceStartText;
    public static string transStartText;


    bool isSubmit = false;

    // Start is called before the first frame update
    void Start()
    {
        sourceRollingText = translationPanel.transform.Find("Source").Find("Text").GetComponent<Text>().text;
        sourceCourageText = "你…有挑戰的勇氣嗎?";
        sourceStartText = startPanel.transform.Find("Button").Find("Text").GetComponent<Text>().text;




        Debug.Log(sourceRollingText);
        Debug.Log(sourceCourageText);
        Debug.Log(sourceStartText);
    }

    // Update is called once per frame
    void Update()
    {
        yinyangObject.transform.Rotate(new Vector3(0, 0, -1f), Space.Self);
        RectTransform r = rollText.GetComponent<RectTransform>();
        if (r.offsetMax.y < 980)
            r.offsetMax = new Vector2(r.offsetMax.x, r.offsetMax.y + 0.5f);
        else
        {
            if (startPanel.gameObject.activeSelf == false&&isSubmit==false)
            {
                ShowRollingTextTranslation();
            }
           
        }

        
        
    }


    public void StartGame()
    {

        if (startPanel.transform.Find("Text").Find("InputField").GetComponent<InputField>().text.Trim() == "")
          {

            wrongPanel.SetActive(true);
            return;


          }

        if (startPanel.transform.Find("Button").Find("Text").Find("InputField").GetComponent<InputField>().text.Trim() == "")
        {

            wrongPanel.SetActive(true);
            return;


        }

        SceneManager.LoadScene("CT_S2");
        Debug.Log("Game on!");

        
    }

    public void ShowEnterGameUI()
    {
        startPanel.gameObject.SetActive(true);
        if (GameObject.Find("SoundPlayer").GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find("SoundPlayer").GetComponent<AudioSource>().Stop();


            if (gameObject.GetComponent<AudioSource>().isPlaying == false)
                gameObject.GetComponent<AudioSource>().Play();

        }
    }


    public void ShowRollingTextTranslation()
    {

        if(translationPanel.activeSelf==false)
        translationPanel.SetActive(true);


    }


    public void SubmitRollingTextTranslation()
    {
        if(translationPanel.transform.Find("Translation").Find("InputField").GetComponent<InputField>().text.Trim()=="")
        {

            wrongPanel.SetActive(true);
            return;
        }



        isSubmit = true;
        transRollingText=translationPanel.transform.Find("Translation").Find("InputField").GetComponent<InputField>().text;
        Debug.Log(transRollingText);


        ShowEnterGameUI();
    }


    public void SubmitStartPanelText()
    {
        if (startPanel.transform.Find("Text").Find("InputField").GetComponent<InputField>().text.Trim()!= "")
        {
            transCourageText = startPanel.transform.Find("Text").Find("InputField").GetComponent<InputField>().text.Trim();


        }

        if (startPanel.transform.Find("Button").Find("Text").Find("InputField").GetComponent<InputField>().text.Trim() != "")
        {
            transStartText = startPanel.transform.Find("Button").Find("Text").Find("InputField").GetComponent<InputField>().text.Trim();
        }

        Debug.Log(transCourageText);
        Debug.Log(transStartText);


        StartGame();
    }
    
    
}
