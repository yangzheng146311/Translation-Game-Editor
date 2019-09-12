using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CT_Fail : MonoBehaviour
{
    public GameObject failTextShow;
    public GameObject failTranslationInput;

    public GameObject errorPanel;

    static public string failSource;
    static public string failTrans;

    int totaljadeNum;


    public GameObject UIText;
    // Start is called before the first frame update
    void Start()
    {
        totaljadeNum = CT_S2.jadeNum;


        UIText.GetComponent<Text>().text = "真可惜，你只成功收集了" + totaljadeNum + "個瑤神美玉…現在請進入「輪迴之境」，鍛煉一番後，歡迎再來挑戰！";

        
        Debug.Log(totaljadeNum);

        failSource = failTextShow.GetComponent<Text>().text;
        Debug.Log(failSource);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ReLoadGame()
    {

        totaljadeNum = 0;
        SceneManager.LoadScene("CT_S2");
    }


    public void SubmitFailTrans()
    {

        if (failTranslationInput.GetComponent<InputField>().text.Trim() == "")
        {
            errorPanel.SetActive(true);
            return;

        }

        failTrans = failTranslationInput.GetComponent<InputField>().text.Trim();
        Debug.Log(failTrans);

        ReLoadGame();
    }
}

