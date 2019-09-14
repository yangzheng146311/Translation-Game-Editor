using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CS_GameManager : MonoBehaviour
{
    static public CS_GameManager gameManager;


    int transID = 0;
    int coins;
    int curLevel;
    public int nextNodeIndex;
    public int lastNodeIndex;
    float curTime;
    float[] maxTime;
    int[] targetCoin;
    string[] levels;
    List<Vector2> playerPos;

    public bool bGameStop;

    Text coinText;
    Text timeText;
    Text levelText;
    public GameObject errorPanel;

    public GameObject player;
    public GameObject trackerGroup;
    private  GameObject successResult;
    private GameObject failResult;
    private GameObject levelIntro;


    public GameObject[] trans;

    public Sprite[] cars;

    string[] sourceText;
    string[] transText;

    public string[] sourcewords;
    public string[] transwords;



    private void Awake()
    {
        sourceText = new string[18];
        transText = new string[18];

        transwords = new string[8];

        gameManager = new CS_GameManager();
       

        coinText = GameObject.Find("Coin_Text").GetComponent<Text>();
        timeText = GameObject.Find("Time_Text").GetComponent<Text>();
        levelText = GameObject.Find("Level_Text").GetComponent<Text>();
    
        gameManager.successResult = GameObject.Find("SuccessResult");
        gameManager.failResult = GameObject.Find("FailResult");
        gameManager.levelIntro = GameObject.Find("level_text");
        


        gameManager.maxTime = new float[5] { 60.0f, 80.0f, 100.0f, 120.0f, 150.0f };
        gameManager.targetCoin = new int[5] { 8, 15, 25, 40, 50 };
        gameManager.levels = new string[5] { "官渡道", "赤壁道", "潼关道", "荆州道", "夷陵道" };


        gameManager.playerPos = new List<Vector2>();
        gameManager.playerPos.Add(new Vector2(57.0f, 65.8f));
        gameManager.playerPos.Add(new Vector2(42.8f, -82.6f));
        gameManager.playerPos.Add(new Vector2(-24.9f, -78.1f));
        gameManager.playerPos.Add(new Vector2(44.9f, -77.4f));
        gameManager.playerPos.Add(new Vector2(44.9f, -77.4f));


        gameManager.bGameStop = true;
        gameManager.curLevel = 1;

      


    }

    // Start is called before the first frame update
    void Start()
    {
        trans[transID].SetActive(true);

        //sourceText[0] = "游戏简介 东汉末年 朝纲混乱。豪杰并起 人才辈出。文臣武将横空出世 共同守护传奇三国。官渡、赤壁、潼关等地相继陷入失守困境 需有识有勇之士速如流星掣电般破局。  三国竞速共设5道关卡 玩家需要在规定的时间内完成关卡任务 进行通关。随着关卡的进程 玩家会陆续解锁不同的道具。  操作介绍 玩家进入游戏后根据赛道选择赛马进行游戏。在游戏中 玩家需要根据关卡任务提示躲避障碍物 收集奖励道具进行加速。";
        for (int i = 1; i < trans.Length; i++)
        {
            string txt = trans[i].transform.Find("Text").GetComponent<Text>().text.Trim();
           
            sourceText[i-1] = txt.Replace(",", " ").Replace("，", " ").Replace("\r", "").Replace("\t", "").Replace("\n", " ").Replace(Environment.NewLine, " ");
            Debug.Log(sourceText[i-1]);

        }


        LoadLevel(gameManager.curLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {

            ReLoadCurLevel();
        }



        CountDown();
        coinText.text = "金币:" + gameManager.coins;
    }

    
    static public CS_GameManager GetManager()
    {

        return gameManager;
    }


    public void AddCoin(int num)
    {

        gameManager.coins += num;
       
        Debug.Log("Coins:" + coins);

    }

    public int GetNextIndex()
    {

        return nextNodeIndex;
    }

    public int GetCurLevel()
    {
        return gameManager.curLevel;

    }
    
    private void CountDown()
    {

        if(gameManager.bGameStop==false)
        {
            if (gameManager.curTime > 0)
            {
                gameManager.curTime -= Time.deltaTime;
                timeText.text = "时间:" + (int)gameManager.curTime;

            }

            else
            {
                if (gameManager.bGameStop==false)
                {
                    LevelFail();
                    gameManager.bGameStop = true;
                }
            }
        }
    }



    public void LoadLevel(int level)
    {

        player.GetComponent<SpriteRenderer>().sprite = cars[level-1];
        player.transform.Rotate(0, 0, (player.transform.eulerAngles.z-180)*-1);

        gameManager.successResult.SetActive(false);
        gameManager.failResult.SetActive(false);
        gameManager.levelIntro.transform.parent.gameObject.SetActive(false);



        gameManager.coins = 0;
        gameManager.nextNodeIndex = 0;
        coinText.text = "金币:" + gameManager.coins; 


       

        levelText.text = gameManager.levels[gameManager.curLevel - 1];
        timeText.text = "时间：" + gameManager.maxTime[gameManager.curLevel - 1];


        gameManager.lastNodeIndex = 6;
        gameManager.curTime = gameManager.maxTime[gameManager.curLevel - 1];
        player.transform.position = gameManager.playerPos[gameManager.curLevel - 1];


        Camera.main.transform.SetPositionAndRotation(new Vector3(player.transform.position.x, player.transform.position.y, -10), Quaternion.identity);

        ShowLevel(gameManager.curLevel - 1);

       
       
    }

    public void StartGame()
    {
        gameManager.bGameStop = false;
    }






    public void EndGame()
    {

        gameManager.bGameStop = true;

        int level = CS_GameManager.GetManager().curLevel;

        Debug.Log(level);


        int targetCoinNum = CS_GameManager.GetManager().targetCoin[level - 1];


        if (gameManager.coins >= targetCoinNum)

            LevelPass();
        else

            LevelFail();

    }


    public void ShowLevel(int level)
    {
        string showLevel = "Tracker_Level" + (level+1);

        for (int i = 0; i < trackerGroup.transform.childCount; i++)
        {
            if(trackerGroup.transform.GetChild(i).name!=showLevel)
            {

                trackerGroup.transform.GetChild(i).gameObject.SetActive(false);

            }

            else
            {

                trackerGroup.transform.GetChild(i).gameObject.SetActive(true);
                
                Transform t= trackerGroup.transform.GetChild(i);

                Transform CoinGroup = t.Find("CoinGroup"); if (CoinGroup) ReSetAllChilds(CoinGroup);
                Transform ConeGroup = t.Find("ConeGroup"); if (ConeGroup) ReSetAllChilds(ConeGroup);
                Transform StartGroup = t.Find("StartGroup"); if (StartGroup) ReSetAllChilds(StartGroup);
                Transform RocketGroup = t.Find("RocketGroup"); if (RocketGroup) ReSetAllChilds(RocketGroup);
                Transform SoilGroup = t.Find("SoilGroup"); if (SoilGroup) ReSetAllChilds(SoilGroup);
                Transform BellGroup = t.Find("BellGroup"); if (BellGroup) ReSetAllChilds(BellGroup);
                Transform GanGroup = t.Find("GanGroup"); if (GanGroup) ReSetAllChilds(GanGroup);


            }
        }
        
    }

    public void LevelPass()
    {

        GameObject.Find("Success Sound").GetComponent<AudioSource>().Play();


        Debug.Log("Win");

        gameManager.successResult.SetActive(true);

        if (gameManager.curLevel != 5)
            gameManager.successResult.transform.Find("Text").GetComponent<Text>().text = "恭喜！您已成功守护" + gameManager.levels[gameManager.curLevel - 1]
                                                                               + "，请马上前往守护" + gameManager.levels[gameManager.curLevel - 1 + 1] + "。";
        else
        {
            gameManager.successResult.transform.Find("Text").GetComponent<Text>().text = "恭喜！您已成功守护所有关卡，请领取您的终极通关奖励。欢迎继续挑战，三国再见！";
            gameManager.successResult.transform.Find("OkBtn").gameObject.SetActive(false);

           

        }

    }

    

    public void LevelFail()
    {
        GameObject.Find("Fail Sound").GetComponent<AudioSource>().Play();
        Debug.Log("Lose");
        gameManager.failResult.SetActive(true);
        gameManager.failResult.SetActive(true);
        gameManager.failResult.transform.Find("Text").GetComponent<Text>().text = "您未能成功守护" + gameManager.levels[gameManager.curLevel - 1]
                                                                          + "，本道即将被攻陷，请选择再次挑战或退出守护。";
     


    }



    private void ReSetAllChilds(Transform t)
    {

        for (int i = 0; i < t.childCount; i++)
        {

            t.GetChild(i).gameObject.SetActive(true);

        }
    }

    public void ReLoadCurLevel()
    {
        int curLevel = gameManager.curLevel;

        LoadLevel(curLevel);


    }
    
    public void GoToNextLevel()
    {

        gameManager.curLevel++;

        LoadLevel(gameManager.curLevel);



    }

    public void ShowLevelIntroText()
    {
        int level = gameManager.curLevel;

        for (int i = 0; i < gameManager.levelIntro.transform.childCount; i++)
        {


            if (gameManager.levelIntro.transform.GetChild(i).name == "LT_" + level)
                gameManager.levelIntro.transform.GetChild(i).gameObject.SetActive(true);
            else
                gameManager.levelIntro.transform.GetChild(i).gameObject.SetActive(false);


        }
    }

    public void QuitGame()
    {

        Application.Quit();

    }

    public void ShowNextTrans()
    {
        

        if(transID==0)
        {

            for (int i = 0; i < 8; i++)
            {
                if(trans[0].transform.Find("Text_" + i.ToString()).Find("InputField").GetComponent<InputField>().text=="")
                {
                    errorPanel.SetActive(true);
                    return;

                }



                transwords[i] = trans[0].transform.Find("Text_" + i.ToString()).Find("InputField").GetComponent<InputField>().text;

                Debug.Log(transwords[i]);
            }


        }

        else
        {
            string txt = trans[transID].transform.Find("InputField").GetComponent<InputField>().text;
            if(txt=="")
            {

                errorPanel.SetActive(true);
                return;
            }

            transText[transID-1] = txt.Trim().Replace(",", " ").Replace("，", " ").Replace("\r", "").Replace("\t", "").Replace("\n", " ").Replace(Environment.NewLine, " ");
            Debug.Log(transText[transID]);




        }






        trans[transID].SetActive(false);








        if (transID != trans.Length - 1)
        {
            transID++;
            trans[transID].SetActive(true);
        }

        else
        {


            ExportText();
        }

        

    }

    public void ExportText()
    {
        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/" + "CS_GAME" + "_Translation.csv";
        Debug.Log(fileName);

        //string fileName = Application.persistentDataPath + "/GameData/" + loadGameName + "_Translation.csv";
        File.WriteAllText(@fileName, string.Empty);

        File.AppendAllText(@fileName, "Simplified Chinese" + ',' + "Translation Text" + Environment.NewLine, System.Text.Encoding.UTF8);
        try
        {

            for (int i = 0; i < 8; i++)
            {
                File.AppendAllText(@fileName, sourcewords[i].Replace(",", " ").Replace("，", " ").Replace("\r", "").Replace("\t", "").Replace("\n", " ").Replace(Environment.NewLine, " ") + ',' + transwords[i].Replace(",", " ").Replace("，", " ").Replace("\r", "").Replace("\t", "").Replace("\n", " ").Replace(Environment.NewLine, " ") + Environment.NewLine, System.Text.Encoding.UTF8);
            }



            for (int i = 0; i < 18; i++)
            {
                File.AppendAllText(@fileName, sourceText[i] + ',' + transText[i] + Environment.NewLine, System.Text.Encoding.UTF8);
            }

           
        }

        catch (Exception err)
        {
            Debug.Log(err);
        }

        Debug.Log("Export Finished");

        System.Diagnostics.Process.Start(fileName);
    }
}


