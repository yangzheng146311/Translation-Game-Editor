using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GameLoadManager : MonoBehaviour
{
    string backgroundFile;
    string objectsFile;
    string characterFile;
    string textFile;


    GameObject Scene;
    GameObject Tpage;

    GameObject Tobject;
    GameObject Tcharacter;

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

        Tcharacter = Resources.Load<GameObject>("Prefabs/character_0");
        Tobject = Resources.Load<GameObject>("Prefabs/Football_0");

        GeneratePages(backgroundFile);
        LoadBackground(backgroundFile);
        LoadObjects(objectsFile);
        LoadCharacters(characterFile);
        LoadText(textFile);
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

        foreach(var x in textListDic)
        {
            for (int i = 0; i<x.Value.Count ; i++)
            {
                Debug.Log(string.Format("{0}.{1} {2}", x.Key, i, x.Value[i]));
            }

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
}
