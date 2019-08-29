using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_GameManager : MonoBehaviour
{
    static public CS_GameManager gameManager;

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


    public GameObject player;
    public GameObject trackerGroup;
    private  GameObject successResult;
    private GameObject failResult;
    private GameObject levelIntro;



    private void Awake()
    {
        gameManager = new CS_GameManager();
       

        coinText = GameObject.Find("Coin_Text").GetComponent<Text>();
        timeText = GameObject.Find("Time_Text").GetComponent<Text>();
        levelText = GameObject.Find("Level_Text").GetComponent<Text>();
    
        gameManager.successResult = GameObject.Find("SuccessResult");
        gameManager.failResult = GameObject.Find("FailResult");
        gameManager.levelIntro = GameObject.Find("level_text");
        


        gameManager.maxTime = new float[5] { 30.0f, 50.0f, 70.0f, 90.0f, 120.0f };
        gameManager.targetCoin = new int[5] { 10, 20, 30, 40, 50 };
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

        LoadLevel(curLevel - 1);


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
}


