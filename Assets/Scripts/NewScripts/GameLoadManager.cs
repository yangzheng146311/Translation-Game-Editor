using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class GameLoadManager : MonoBehaviour
{
    string backgroundFile;
    string objectsFile;
    string characterFile;
    string textFile;

    int curPageIndex;
    int curSubtileIndex;

    GameObject Scene;
    GameObject Tpage;

    List<GameObject> pageList;

    GameObject Tobject;
    GameObject Tcharacter;

    GameObject subtitle;

    Dictionary<int, List<string>> textListDic;


    // Start is called before the first frame update
    void Start()
    {
        backgroundFile = "Assets/GameLoad/_Background.txt";
        objectsFile = "Assets/GameLoad/_Obj.txt";
        characterFile = "Assets/GameLoad/_Character.txt";
        textFile = "Assets/GameLoad/_ingametext.txt";

        Scene = GameObject.Find("Scene");
        Tpage = GameObject.Find("Page_1");
        subtitle = GameObject.Find("Subtitle");

        Tcharacter = Resources.Load<GameObject>("Prefabs/character_0");
        Tobject = Resources.Load<GameObject>("Prefabs/Football_0");

        GeneratePages(backgroundFile);
        LoadBackground(backgroundFile);
        LoadObjects(objectsFile);
        LoadCharacters(characterFile);
        LoadText(textFile);


        pageList = new List<GameObject>();

        for (int i = 0; i < Scene.transform.childCount; i++)
        {
            pageList.Add(Scene.transform.GetChild(i).gameObject);
        }


        curPageIndex = 1;
        curSubtileIndex = 0;
        try
        {

            string curtext = textListDic[curPageIndex][curSubtileIndex];
            subtitle.GetComponent<Text>().text = curtext;
        }
        catch (Exception err)
        {
            throw err;
        }

        ShowPage("Page_1");
        
    }

    private void LoadCharacters(string fileName)
    {
        int line = 1;
        StreamReader streamReader = new StreamReader(@fileName);

        bool EndOfFile = false;
        while (!EndOfFile)
        {

            string data_string = streamReader.ReadLine();
            if (data_string == null)
            {

                EndOfFile = true;
                break;

            }

            if (line != 1)
            {
                string[] data_value = data_string.Split(',');

                int pageID = int.Parse(data_value[0]);
                string CharacterName = data_value[1];        
                float Pos_X = float.Parse(data_value[2]);
                float Pos_Y = float.Parse(data_value[3]);
                float Scale = float.Parse(data_value[4]);
                string Hat = data_value[5];
                string Head = data_value[6];
                string Body = data_value[7];
                string Leg = data_value[8];
             
                Sprite hatSprite = Resources.Load<Sprite>("Art assets/Characters art/Individual character parts/" + Hat);
                Sprite HeadSprite = Resources.Load<Sprite>("Art assets/Characters art/Individual character parts/" + Head);
                Sprite BodySprite = Resources.Load<Sprite>("Art assets/Characters art/Individual character parts/" + Body);
                Sprite LegSprite = Resources.Load<Sprite>("Art assets/Characters art/Individual character parts/" + Leg);

                Transform page = Scene.transform.Find("Page_" + pageID).Find("Characters");
                GameObject obj = Instantiate(Tcharacter, page);
                obj.name = CharacterName;
                obj.transform.position = new Vector3(Pos_X, Pos_Y, Tobject.transform.position.z);
                obj.transform.localScale = new Vector3(1.0f * Scale, 1.0f * Scale, 1.0f * Scale);

                obj.transform.Find("Hat").GetComponent<SpriteRenderer>().sprite = hatSprite;
                obj.transform.Find("Head").GetComponent<SpriteRenderer>().sprite = HeadSprite;
                obj.transform.Find("Body").GetComponent<SpriteRenderer>().sprite = BodySprite;
                obj.transform.Find("Legs").GetComponent<SpriteRenderer>().sprite = LegSprite;


            }
            line++;

        }
    }

    private void LoadObjects(string fileName)
    {
        int line = 1;
        StreamReader streamReader = new StreamReader(@fileName);

        bool EndOfFile = false;
        while (!EndOfFile)
        {

            string data_string = streamReader.ReadLine();
            if (data_string == null)
            {

                EndOfFile = true;
                break;

            }

            if (line != 1)
            {
                string[] data_value = data_string.Split(',');

                int pageID = int.Parse(data_value[0]);
                string ObjectName = data_value[1];
                string SpriteName = data_value[2];
                float Pos_X = float.Parse(data_value[3]);
                float Pos_Y = float.Parse(data_value[4]);
                float Scale = float.Parse(data_value[5]);

                Sprite objSprite = Resources.Load<Sprite>("Art assets/Objects art/" + SpriteName);
                Transform page = Scene.transform.Find("Page_" + pageID).Find("Objects");
                GameObject obj = Instantiate(Tobject, page);
                obj.name = ObjectName;
                obj.GetComponent<SpriteRenderer>().sprite = objSprite;
                obj.transform.position = new Vector3(Pos_X, Pos_Y, Tobject.transform.position.z);

                obj.transform.localScale = new Vector3(1.0f * Scale, 1.0f * Scale, 1.0f * Scale);
             
            }
            line++;

        }
    }

    private void LoadBackground(string fileName)
    {
        int line = 1;
        StreamReader streamReader = new StreamReader(@fileName);

        bool EndOfFile = false;
        while (!EndOfFile)
        {

            string data_string = streamReader.ReadLine();
            if (data_string == null)
            {

                EndOfFile = true;
                break;

            }

            if (line != 1)
            {
                string[] data_value = data_string.Split(',');

                int pageID = int.Parse(data_value[0]);
                string background = data_value[1];

                Sprite sprite = Resources.Load<Sprite>("Art assets/Background art/"+background);
                Scene.transform.Find("Page_" + pageID.ToString()).Find("background").GetComponent<SpriteRenderer>().sprite = sprite;


            }


            line++;

        }
    }

    private void LoadText(string fileName)
    {
        textListDic = new Dictionary<int, List<string>>();
        int line = 1;
        StreamReader streamReader = new StreamReader(@fileName);

        bool EndOfFile = false;
        while (!EndOfFile)
        {

            string data_string = streamReader.ReadLine();
            if (data_string == null)
            {

                EndOfFile = true;
                break;

            }

            if (line != 1)
            {
                string[] data_value = data_string.Split(',');

                int pageID = int.Parse(data_value[0]);
                int textID= int.Parse(data_value[1]);
                string text = data_value[2];

                if(textListDic.ContainsKey(pageID))
                {

                    textListDic[pageID].Add(text);

                }
                
                else
                {
                    List<string> textList = new List<string>();
                    textListDic.Add(pageID, textList);
                    textListDic[pageID].Add(text);
                }
            }
            line++;
        }

      
    }

    void GeneratePages(string fileName)
    {
        int PageNum = -1;
        StreamReader streamReader = new StreamReader(@fileName);

        bool EndOfFile = false;
        while (!EndOfFile)
        {

            string data_string = streamReader.ReadLine();
            if (data_string == null)
            {

                EndOfFile = true;
                break;

            }

            PageNum++;
           
        }
       

        if (PageNum > 1)
        {

            for (int i = 2; i <= PageNum; i++)
            {

                GameObject page = Instantiate(Tpage, Scene.transform);
                page.name = "Page_" + i.ToString();
               


            }
        }
    }

    public void NextSubtitle()
    {

        if (textListDic.ContainsKey(curPageIndex))
        {
            if (curSubtileIndex + 1 <= textListDic[curPageIndex].Count - 1)
            {
                curSubtileIndex++;
                string nexttext = textListDic[curPageIndex][curSubtileIndex];

                if (nexttext != "")
                    subtitle.GetComponent<Text>().text = nexttext;
                else
                    subtitle.GetComponent<Text>().text = "(Slient)";
            }
        }
        else
        {

            subtitle.GetComponent<Text>().text = "(Current page have no text)";
        }
    }
    public void ShowPage(string pageName)
    {

        foreach (GameObject g in pageList)
        {

            if (g.name != pageName)
            {

                g.SetActive(false);
            }

            else
            {

                g.SetActive(true);

            }
        }


    }
    public void NextPage()
    {
         
        

        if (curPageIndex+1 <= Scene.transform.childCount)
        {
            curPageIndex++;
            curSubtileIndex = 0;
            ShowPage("Page_" + curPageIndex.ToString());



            if (textListDic.ContainsKey(curPageIndex))
            {
                if (textListDic[curPageIndex].Count > 0)
                {

                    string nexttext = textListDic[curPageIndex][curSubtileIndex];
                    if (nexttext != "")
                        subtitle.GetComponent<Text>().text = nexttext;
                    else
                        subtitle.GetComponent<Text>().text = "(Slient)";
                }
            }
            else
            {
                subtitle.GetComponent<Text>().text = "(Current page have no text)";

            }
        }


        


    }
}
