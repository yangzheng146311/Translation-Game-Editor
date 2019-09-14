using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CT_S2 : MonoBehaviour
{
    Vector3 mTargetPos;
    public GameObject character;
    public GameObject LifeText;
    public GameObject errorPanel;

    public static string sourcePlayWay;
    public static string transPlayWay;

    public static string sourceTips;
    public static string transTips;

    public static string sourceEnd;
    public static string transEnd;

    public static string sourcePlayWayTxt;
    public static string transPlayWayTxt;

    public static string sourceTipsTxt;
    public static string transTipsTxt;


    Vector2 playerPos;
    Vector2 hitPos;
    Vector2 dir;
    float time;
    int life = 5;
    public GameObject jadeUI;
    public static int jadeNum = 0;
    public Transform[] pairs;

    public bool[] pairsFound;

    bool showUI = false;

    bool actionFinish = true;

    bool bsubmitPlayWay = false;
    bool bsubmitTips = false;



    // Start is called before the first frame update
    void Start()
    {

        sourcePlayWay = GameObject.Find("PlaywayTxt").GetComponent<Text>().text;
        sourceTips = GameObject.Find("TipsTxt").GetComponent<Text>().text;
        sourceEnd = GameObject.Find("EndTxt").GetComponent<Text>().text;







        jadeNum = 0;
        life = 5;

        for (int i = 0; i < pairsFound.Length; i++)
        {
            pairsFound[i] = false;
        }


        playerPos = (Vector2)character.transform.position;
        hitPos = playerPos;
        dir = Vector2.zero;

        time = 0;
        showUI = false;

        actionFinish = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (jadeUI.activeSelf)
        {

            time -= Time.deltaTime;
            if (time <= 0)
            {
                jadeUI.SetActive(false);
                showUI = false;

            }
        }


        playerPos = (Vector2)character.transform.position;

        //按下鼠标右键时
        if (Input.GetMouseButtonDown(1) && bsubmitTips && bsubmitPlayWay)
        {
            actionFinish = false;

            //获取屏幕坐标
            Vector3 mScreenPos = Input.mousePosition;
            //定义射线
            Ray mRay = Camera.main.ScreenPointToRay(mScreenPos);

            //判断射线是否击中Background


            RaycastHit2D hit = Physics2D.Raycast(mRay.origin, mRay.direction, Mathf.Infinity);


            hitPos = hit.point;

        }



        Vector2 dir = (hitPos - playerPos).normalized;

        if (Mathf.Abs(hitPos.x - playerPos.x) > 0.5f || Mathf.Abs(hitPos.y - playerPos.y) > 0.5f)
        {

            character.transform.Translate(dir * 0.1f);
        }


        else
        {


            //判断是否找到玉石
            for (int i = 0; i < pairs.Length; i++)
            {
                if (Vector2.Distance(playerPos, (Vector2)pairs[i].position) < 3.0f)
                {


                    //ShowPairs(pairs[i]);
                    if (pairsFound[i] == false)
                    {
                        pairsFound[i] = true;
                        ShowJadeUI();
                        actionFinish = true;
                    }
                }
            }

            if (actionFinish == false)
            {

                actionFinish = true;


                if (life > 0)
                {
                    life--;
                    LifeText.GetComponent<Text>().text = "生命x" + life.ToString();
                }

                else
                {

                    Debug.Log("Die");
                    SceneManager.LoadScene("CT_Fail");


                }




            }


        }





    }
    void ShowJadeUI()
    {

        if (showUI == false)
        {

            showUI = true;

            time = 3.0f;
            jadeUI.SetActive(true);
            jadeNum++;
            jadeUI.GetComponent<Text>().text = "瑤神美玉x" + jadeNum.ToString();
            Debug.Log(jadeNum);
            if (jadeNum >= 3)
            {
                Debug.Log("Success");
                SceneManager.LoadScene("CT_Success");

            }
        }

        else
        {

            time = 3.0f;
            jadeUI.SetActive(true);
            jadeNum++;
            Debug.Log(jadeNum);
            jadeUI.GetComponent<Text>().text = "瑤神美玉x" + jadeNum.ToString();
            if (jadeNum >= 3)
            {
                Debug.Log("Success");
                SceneManager.LoadScene("CT_Success");

            }

        }


    }


    void ShowPairs(Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).GetComponent<SpriteRenderer>().enabled == false)
                t.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
        }
    }


    public void SubmitThreeWords()
    {
        if (GameObject.Find("PlaywayTxt").transform.Find("InputField").GetComponent<InputField>().text.Trim() == "")
        {
            errorPanel.SetActive(true);
            return;

        }

        if (GameObject.Find("TipsTxt").transform.Find("InputField").GetComponent<InputField>().text.Trim() == "")
        {
            errorPanel.SetActive(true);
            return;

        }

        if (GameObject.Find("EndTxt").transform.Find("InputField").GetComponent<InputField>().text.Trim() == "")
        {
            errorPanel.SetActive(true);
            return;

        }

        transPlayWay = GameObject.Find("PlaywayTxt").transform.Find("InputField").GetComponent<InputField>().text.Trim();
        transTips = GameObject.Find("TipsTxt").transform.Find("InputField").GetComponent<InputField>().text.Trim();
        transEnd = GameObject.Find("EndTxt").transform.Find("InputField").GetComponent<InputField>().text.Trim();

        Debug.Log(transPlayWay);
        Debug.Log(transTips);
        Debug.Log(transEnd);
        GameObject.Find("Threewords").SetActive(false);
    }


    public void ShowPlayWayPanel()
    {

        GameObject.Find("PlayWayPanel").SetActive(true);
    }

    public void ShowTipsPanel()
    {

        GameObject.Find("TipsPanel").SetActive(true);
    }

    public void SubmitPlayWay()
    {
        if (GameObject.Find("PlayWayPanel").transform.Find("InputField").GetComponent<InputField>().text.Trim() == "")
        {

            errorPanel.SetActive(true);
            return;
        }

        else
        {
            transPlayWayTxt = GameObject.Find("PlayWayPanel").transform.Find("InputField").GetComponent<InputField>().text.Trim();
            sourcePlayWayTxt = "右鍵點擊任何你認為有可能藏有瑤神美玉的地方，收集三枚美玉就能通關。你的起始生命是5點，每找錯一次便會失去1點生命。當你失去全部生命時，就會化為煙塵，遊戲結束。";


            Debug.Log(sourcePlayWayTxt.Replace(" ", "/"));
            Debug.Log(transPlayWayTxt);
            GameObject.Find("PlayWayPanel").SetActive(false);

            bsubmitPlayWay = true;

        }



    }

    public void SubmitTips()
    {

        if (GameObject.Find("TipsPanel").transform.Find("InputField").GetComponent<InputField>().text.Trim() == "")
        {

            errorPanel.SetActive(true);
            return;
        }

        else
        {
            transTipsTxt = GameObject.Find("TipsPanel").transform.Find("InputField").GetComponent<InputField>().text.Trim();
            sourceTipsTxt = GameObject.Find("TipsPanel").transform.Find("Text").GetComponent<Text>().text.Trim();
            Debug.Log(sourceTipsTxt);
            Debug.Log(transTipsTxt);
            GameObject.Find("TipsPanel").SetActive(false);

            bsubmitTips = true;

        }

       
    }
    public void ENDGame()
    {
        Application.Quit(); 

    }

}
