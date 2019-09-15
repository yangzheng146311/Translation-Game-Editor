 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    GameObject inputField;

    AudioSource myaudio;

    GameObject lastBtn;
    GameObject nextBtn;
    GameObject subBtn;
    GameObject lastSubtitleBtn;
    GameObject nextSubtitleBtn;

   List<AudioClip> audioList;


    Dictionary<int, List<string>> textListDic;
    Dictionary<int, List<string>> translateListDic;
    List<string> musicNameList;
    string loadGameName;
    // Start is called before the first frame update
    void Start()
    {
         loadGameName = MenuEvent.loadingGameName;

        pageList = new List<GameObject>();
        musicNameList = new List<string>();
        audioList = new List<AudioClip>();
        backgroundFile = Application.persistentDataPath + "/GameData/" + loadGameName+"/_Background.txt";
        objectsFile = Application.persistentDataPath + "/GameData/" + loadGameName + "/_Obj.txt";
        characterFile = Application.persistentDataPath + "/GameData/" + loadGameName + "/_Character.txt";
        textFile = Application.persistentDataPath + "/GameData/" + loadGameName + "/_ingametext.txt";

        Scene = GameObject.Find("Scene");
        Tpage = GameObject.Find("Page_1");
        subtitle = GameObject.Find("Subtitle");
        inputField = GameObject.Find("PlayerInput");
        inputField.gameObject.SetActive(false);
        lastBtn = GameObject.Find("BtnLastPage");
        nextBtn = GameObject.Find("BtnNextPage");
        subBtn = GameObject.Find("BtnSaveAndQuit");
        nextSubtitleBtn = GameObject.Find("BtnNextSubtile");
        lastSubtitleBtn = GameObject.Find("BtnLastSubtitle");


        Tcharacter = Resources.Load<GameObject>("Prefabs/character_0");
        Tobject = Resources.Load<GameObject>("Prefabs/Football_0");

        myaudio = gameObject.GetComponent<AudioSource>();

        GeneratePages(backgroundFile);
        LoadBackground(backgroundFile);
        LoadObjects(objectsFile);
        LoadCharacters(characterFile);
        LoadText(textFile);




        for (int i = 0; i < Scene.transform.childCount; i++)
        {
            pageList.Add(Scene.transform.GetChild(i).gameObject);
        }


        curPageIndex = 1;
        curSubtileIndex = 0;
        try
        {

            string curtext = textListDic[curPageIndex][curSubtileIndex];

            if (curtext == "") curtext = "(Slient)";
            subtitle.GetComponent<Text>().text = curtext;
        }
        catch (Exception err)
        {
            throw err;
        }

        ShowPage("Page_1");

        //AudioPlay();


    }

    private void Update()
    {

       

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inputField.activeSelf == false)
            {
                inputField.GetComponent<InputField>().text = "";
                inputField.SetActive(true);
            }


            else
            {
                inputField.SetActive(false);
                if(inputField.GetComponent<InputField>().text.Trim()!="")
                subtitle.GetComponent<Text>().text = inputField.GetComponent<InputField>().text;

            }
        }
        AudioPlay();

        ArrowBtnControl();

    }

    private void ArrowBtnControl()
    {
        if (curPageIndex == 1&&curPageIndex != pageList.Count)
        {
            if (lastBtn.activeSelf == true)
                lastBtn.SetActive(false);

            if (subBtn.activeSelf == true)
                subBtn.SetActive(false);

            if (nextBtn.activeSelf == false)
                nextBtn.SetActive(true);
        }

        if (curPageIndex == pageList.Count&& curPageIndex !=1)
        {

            if (nextBtn.activeSelf == true)
                nextBtn.SetActive(false);

            if (subBtn.activeSelf == false)
                subBtn.SetActive(true);

            if (lastBtn.activeSelf == false)
                lastBtn.SetActive(true);

        }

        if(curPageIndex==1&&curPageIndex==pageList.Count)
        {

            if (lastBtn.activeSelf == true)
                lastBtn.SetActive(false);

            if (nextBtn.activeSelf == true)
                nextBtn.SetActive(false);

            if (subBtn.activeSelf == false)
                subBtn.SetActive(true);
        }

        if (curPageIndex > 1 && curPageIndex < pageList.Count)
        {
            if (lastBtn.activeSelf == false)
                lastBtn.SetActive(true);

            if (nextBtn.activeSelf == false)
                nextBtn.SetActive(true);

            if (subBtn.activeSelf == true)
                subBtn.SetActive(false);

        }


        if(curSubtileIndex==0&&curSubtileIndex!= translateListDic[curPageIndex].Count - 1)
        {
            if(lastSubtitleBtn.activeSelf==true) lastSubtitleBtn.SetActive(false);
            if (nextSubtitleBtn.activeSelf ==false)  nextSubtitleBtn.SetActive(true);

        }

        if(curSubtileIndex>0&&curSubtileIndex<translateListDic[curPageIndex].Count-1)
        {
            if (lastSubtitleBtn.activeSelf == false) lastSubtitleBtn.SetActive(true);
            if (nextSubtitleBtn.activeSelf == false) nextSubtitleBtn.SetActive(true);

        }

        if(curSubtileIndex== translateListDic[curPageIndex].Count - 1&&curSubtileIndex!=0)
        {

            if (lastSubtitleBtn.activeSelf == false) lastSubtitleBtn.SetActive(true);
            if (nextSubtitleBtn.activeSelf == true) nextSubtitleBtn.SetActive(false);
        }

        if(curSubtileIndex==0&&curSubtileIndex== translateListDic[curPageIndex].Count - 1)
        {
            if (lastSubtitleBtn.activeSelf == true) lastSubtitleBtn.SetActive(false);
            if (nextSubtitleBtn.activeSelf == true) nextSubtitleBtn.SetActive(false);

        }


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
                obj.GetComponent<DragableObjects>().enabled = false;

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
                obj.GetComponent<DragableObjects>().enabled = false;
             
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

                string musicName = "";
                if (data_value[2]!="")
                 musicName = data_value[2];

                Sprite sprite = Resources.Load<Sprite>("Art assets/Background art/"+background);

                if(!sprite)
                {
                    string folderpath = Application.persistentDataPath + "/GameResources/GameImage/";
                    string igFileName = folderpath + background+".jpg";
                    byte[] fileData = getImageByte(igFileName);
                    Texture2D t2d = new Texture2D(2, 2);
                    //根据路劲读取字节流再转换成图片形式
                    t2d.LoadImage(fileData);
                    sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f));
                    sprite.name = background;
                }

                Scene.transform.Find("Page_" + pageID.ToString()).Find("background").GetComponent<SpriteRenderer>().sprite = sprite;

                Scene.transform.Find("Page_" + pageID.ToString()).Find("background").GetComponent<FullScreenSprite>().Fit();

                musicNameList.Add(musicName);
                LoadBGM(musicName);

            }


            line++;

        }
    }

    private void LoadText(string fileName)
    {
        textListDic = new Dictionary<int, List<string>>();
        translateListDic= new Dictionary<int, List<string>>();
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

                string replace = text.Replace("/", ",");


                if(textListDic.ContainsKey(pageID))
                {

                    textListDic[pageID].Add(replace);
                    
                    

                }
                
                else
                {
                    List<string> textList = new List<string>();
                    textListDic.Add(pageID, textList);
                    textListDic[pageID].Add(replace);
                    


                }

                if (translateListDic.ContainsKey(pageID))
                {

                    translateListDic[pageID].Add(replace);
                  


                }

                else
                {
                    List<string> textList = new List<string>();
                    translateListDic.Add(pageID, textList);
                    translateListDic[pageID].Add(replace);


                }
            }
            line++;
        }

      
    }

    private void GeneratePages(string fileName)
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
        


        string text = subtitle.GetComponent<Text>().text;


        if (text.Equals(""))
        {
            
            subtitle.GetComponent<Text>().text = "(You need to write down your answer!!!)";
            return;

        }

        translateListDic[curPageIndex][curSubtileIndex] = text;

        if (textListDic.ContainsKey(curPageIndex))
        {
            if (curSubtileIndex + 1 <= translateListDic[curPageIndex].Count - 1)
            {
                curSubtileIndex++;
                string nexttext = translateListDic[curPageIndex][curSubtileIndex];

               

                if (nexttext != "")
                    subtitle.GetComponent<Text>().text = nexttext;
                else
                    subtitle.GetComponent<Text>().text = "(Slient)";
            }

            //else
            //{

            //    if (curPageIndex >= translateListDic.Count)
            //    {
            //        Debug.Log("End");
            //        ExportTranslationText();

            //    }

            //}
            
        }
        else
        {

            subtitle.GetComponent<Text>().text = "(Current page have no text)";
        }
    }

    public void LastSubtitle()
    {
        string text = subtitle.GetComponent<Text>().text;
        if (text.Equals(""))
        {
            subtitle.GetComponent<Text>().text = "(You need to write down your answer!!!)";
            return;

        }


        translateListDic[curPageIndex][curSubtileIndex] = text;

        if (translateListDic.ContainsKey(curPageIndex))
        {
            if (curSubtileIndex - 1 >=0)
            {
                curSubtileIndex--;
                string lasttext = translateListDic[curPageIndex][curSubtileIndex];



                if (lasttext != "")
                    subtitle.GetComponent<Text>().text = lasttext;
                else
                    subtitle.GetComponent<Text>().text = "(Slient)";
            }
            

        }
        else
        {

            subtitle.GetComponent<Text>().text = "(Current page have no text)";
        }




    }

    public void ReSetSubtitle()
    {
        if (textListDic[curPageIndex][curSubtileIndex] != "")
            subtitle.GetComponent<Text>().text = textListDic[curPageIndex][curSubtileIndex];
        else
            subtitle.GetComponent<Text>().text = "(Slient)";
    }

    public void ShowPage(string pageName)
    {


        myaudio.clip = null;

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

    public void LastPage()
    {

        if (subtitle.GetComponent<Text>().text.Equals(""))
        {
            subtitle.GetComponent<Text>().text = "(You need to write down your answer!!!)";
            return;

        }
        translateListDic[curPageIndex][curSubtileIndex] = subtitle.GetComponent<Text>().text;

        if (curPageIndex - 1 >= 1)
        {
            curPageIndex--;
            curSubtileIndex = 0;
            ShowPage("Page_" + curPageIndex.ToString());
            //AudioPlay();


            if (translateListDic.ContainsKey(curPageIndex))
            {
                if (translateListDic[curPageIndex].Count > 0)
                {

                    string text = translateListDic[curPageIndex][curSubtileIndex];
                    if (text != "")
                        subtitle.GetComponent<Text>().text = text;
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

    public void NextPage()
    {
        if (subtitle.GetComponent<Text>().text.Equals(""))
        {
            subtitle.GetComponent<Text>().text = "(You need to write down your answer!!!)";
            return;

        }
        translateListDic[curPageIndex][curSubtileIndex] = subtitle.GetComponent<Text>().text;


        if (curPageIndex+1 <= Scene.transform.childCount)
        {
            curPageIndex++;
            curSubtileIndex = 0;
            ShowPage("Page_" + curPageIndex.ToString());
            //AudioPlay();


            if (translateListDic.ContainsKey(curPageIndex))
            {
                if (translateListDic[curPageIndex].Count > 0)
                {

                    string nexttext = translateListDic[curPageIndex][curSubtileIndex];
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

    public void Submit()
    {
        string text = subtitle.GetComponent<Text>().text;
        if (text.Equals(""))
        {
            subtitle.GetComponent<Text>().text = "(You need to write down your answer!!!)";
            return;

        }


        translateListDic[curPageIndex][curSubtileIndex] = text;
        ExportTranslationText();

        
        SceneManager.LoadScene("Entrance");
    }

    public void ExportTranslationText()
    {
       
        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"/" + loadGameName + "_Translation.csv";
        Debug.Log(fileName);

        //string fileName = Application.persistentDataPath + "/GameData/" + loadGameName + "_Translation.csv";
        File.WriteAllText(@fileName, string.Empty);

        try
        {
            using (System.IO.StreamWriter file = new StreamWriter(@fileName, true))
            {
               file.WriteLine("PageID" + ',' + "TextID" + ','+"SourceText" + ','+"Translation"); ;

                for (int i = 1; i <=translateListDic.Count; i++)
                {

                    for (int j = 0; j < translateListDic[i].Count; j++)
                    {

                        string replace_text = textListDic[i][j].Replace(",", "/");
                        string replace_trans = translateListDic[i][j].Replace(",", "/");


                        file.WriteLine((i).ToString() + ',' + (j+1).ToString() + ',' + replace_text + ',' + replace_trans); ;
                    }
                }
               
            }
        }
        catch (Exception ex)
        {

            throw new ApplicationException("error:", ex);

        }

        Debug.Log("Export Finished");

        System.Diagnostics.Process.Start(fileName);
    }

    private void LoadBGM(string musicName)
    {

        AudioClip clip = Resources.Load<AudioClip>("Audio/" + musicName);
       

        if (!clip)
        {


            string ExternalImageFilePath = Application.persistentDataPath + "/GameResources/GameMusic/";
            string igfileName = ExternalImageFilePath +musicName;
            //Debug.Log(igfileName);
            StartCoroutine(LoadAuido(igfileName));

           
           

        }
        else { 

        clip.name = musicName;
        audioList.Add(clip);
            }
        //clip.name = musicName;


        //myaudio.clip = clip;
    }

    private void AudioPlay()
    {


        Debug.Log(audioList.Count);

        if (audioList.Count > 0)
        {

            int targetMusicIndex = curPageIndex - 1;
            if (targetMusicIndex >= 0 && targetMusicIndex <= pageList.Count - 1)
            {


                if (audioList[targetMusicIndex])
                {

                   
                    if (!myaudio.clip) myaudio.clip = audioList[targetMusicIndex];


                    if (myaudio.isPlaying == false)
                    {

                        myaudio.Play();

                    }
                }
                else
                {
                    myaudio.clip = null;


                }



                //string music = musicNameList[curPageIndex - 1];

                ////Debug.Log(music);
                //if (music != "")
                //{

                //    if(!myaudio.clip) LoadBGM(music);

                //    if (myaudio.isPlaying == false)
                //    {

                //        myaudio.Play();

                //    }
                //}
            }
        }

    }

    private static byte[] getImageByte(string imagePath)
    {
        //读取到文件
        FileStream files = new FileStream(imagePath, FileMode.Open);
        //新建比特流对象
        byte[] imgByte = new byte[files.Length];
        //将文件写入对应比特流对象
        files.Read(imgByte, 0, imgByte.Length);
        //关闭文件
        files.Close();
        //返回比特流的值
        return imgByte;
    }

    private IEnumerator LoadAuido(string audiopath)
    {


       

        string audioName = audiopath.Split('/')[audiopath.Split('/').Length - 1];

        WWW request = GetAudioFromFile(audiopath);
        yield return request;


       AudioClip audioClip = NAudioPlayer.FromMp3Data(request.bytes);

        audioName = audioName.Split('.')[0];
        audioClip.name = audioName;
        
        audioList.Add(audioClip);
    }

    private WWW GetAudioFromFile(string audiopath)
    {
        WWW reqeust = new WWW(audiopath);
        return reqeust;
    }

}
