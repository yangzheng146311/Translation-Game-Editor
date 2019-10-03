using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEditor;
using SimpleFileBrowser;
public class EditManager : MonoBehaviour
{

    static public EditManager editManager;
    private Dictionary<string, Sprite> spriteDic;
    public Dictionary<string, GameObject> sceneObjects;
    private Dictionary<string, int> objTypes;
    public List<string> pageMusicList;
    private GameObject Tpage;
    private GameObject TpageImage;
    public List<GameObject> pageList;

    private Dictionary<string,AudioClip> tempMusicList;


    public GameObject textCreatorList;
    GameObject Ttextcreator;
    List<List<string>> sceneTextList;
    public int curPageIndex;
    public string curPageMusicName;

    public GameObject emptyTextPanel;

    string gameTitle = "UnNameGame";

    private string path;
    
    // Start is called before the first frame update
    void Start()
    {

       



        curPageIndex = 0;
        Tpage = GameObject.Find("Page_1");
        sceneObjects = new Dictionary<string, GameObject>();
        objTypes = new Dictionary<string, int>();
        spriteDic = new Dictionary<string, Sprite>();
        pageList = new List<GameObject>();
        pageList.Add(Tpage);

        sceneTextList = new List<List<string>>();
        List<string> textList = new List<string>();
        textList.Add("");
        sceneTextList.Add(textList);

        tempMusicList = new Dictionary<string, AudioClip>();
       
        Ttextcreator = Resources.Load<GameObject>("Prefabs/TextCreator");

        pageMusicList = new List<string>();
        pageMusicList.Add("");

        Sprite[] sprites = Resources.LoadAll<Sprite>("Art assets/Objects art");
        foreach(Sprite s in sprites)
        {

            spriteDic.Add(s.name, s);
        }

        //InstantiateSprite("Football");
        //Debug.Log("type"+objTypes["Football"]);
        //Debug.Log("count"+sceneObjects.Count);


        //DestroySprite("Football_0");
        //Debug.Log("type" + objTypes["Football"]);
        //Debug.Log("count" + sceneObjects.Count);


        editManager = this;
    }

    // Update is called once per frame
    void Update()
    {


       // Debug.Log(GameObject.Find("SoundManager").GetComponent<AudioSource>().isPlaying);

        RefreshPagesID();
        RefreshTextList();
        AudioPlay();



    } 

    static public EditManager GetEditManager()
    {

        
            return editManager;


    }

     public GameObject InstantiateSprite(string spriteName)
    {

        GameObject obj= new GameObject();
        SpriteRenderer renderer =obj.AddComponent<SpriteRenderer>();
        renderer.sprite = spriteDic[spriteName];
        obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);



        //if the object is not exist,is a new object type else add the amount of relevant type 
        if(objTypes.ContainsKey(spriteName)==false)
        {
            objTypes.Add(spriteName, 0);      
        }
        else
        {
            objTypes[spriteName] += 1;         
        }
        obj.name = spriteName + "_" + objTypes[spriteName];
        sceneObjects.Add(obj.name, obj);
        obj.AddComponent<BoxCollider2D>();
        obj.AddComponent<DragableObjects>();
        
        obj.transform.SetParent(GameObject.Find("Objects").transform);
        return obj;
    }


    public GameObject InstantiateSprite(Sprite sprite)
    {

        GameObject obj = new GameObject();
        SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);



        //if the object is not exist,is a new object type else add the amount of relevant type 
        if (objTypes.ContainsKey(sprite.name) == false)
        {
            objTypes.Add(sprite.name, 0);
        }
        else
        {
            objTypes[sprite.name] += 1;
        }
        obj.name = sprite.name + "_" + objTypes[sprite.name];
        sceneObjects.Add(obj.name, obj);
        obj.AddComponent<BoxCollider2D>();
        obj.AddComponent<DragableObjects>();

        obj.transform.SetParent(GameObject.Find("Objects").transform);
        return obj;
    }


    public void DestroySprite(string name)
    {
        Destroy(sceneObjects[name]); 
        sceneObjects.Remove(name);
        int index = name.IndexOf('_');
        
        //if(objTypes[name.Substring(0, index)]>0)
        //objTypes[name.Substring(0,index)]--;


    }


    public void CreateCharacter()
    {

        GameObject character = GameObject.Find("character");

        GameObject newCharcter = Instantiate(character);

        if (objTypes.ContainsKey("character") == false)
        {
            objTypes.Add("character", 0);
        }
        else
        {
            objTypes["character"] += 1;
        }
        newCharcter.name = "character" + "_" + objTypes["character"];
        sceneObjects.Add(newCharcter.name, newCharcter);
        //BoxCollider2D box =newCharcter.AddComponent<BoxCollider2D>();
        //box.transform.localPosition = new Vector3(1, 1, 1);
        newCharcter.AddComponent<DragableObjects>();

        newCharcter.transform.SetParent(GameObject.Find("Characters").transform);



    }


    public void HideAllObjects()
    {



        foreach (var x in sceneObjects)
            x.Value.SetActive(false);

    }

    public void ShowAllObjects()
    {

        foreach (var x in sceneObjects)
            x.Value.SetActive(true);

    }

    //
    public void   SavePage()
    {
        Debug.Log("SavePage");
        if (!Directory.Exists(System.Environment.CurrentDirectory + "/GameData/" + gameTitle))
        {
            Directory.CreateDirectory(System.Environment.CurrentDirectory + "/GameData/" + gameTitle);
        }

        string GameFilePath = System.Environment.CurrentDirectory + "/GameData/" + gameTitle;
        string bgfileName = GameFilePath + "/_Background.txt";
        string objfileName = GameFilePath + "/_Obj.txt";
        string charcfileName = GameFilePath + "/_Character.txt";
        string inGameTextFileName = GameFilePath + "/_ingametext.txt";


      


        GameObject Scene = GameObject.Find("Scene");

        bool bBackground = false;
        bool bObject = false;
        bool bCharacter = false;
        bool btext = false;
        File.WriteAllText(@bgfileName, string.Empty);
        File.WriteAllText(@objfileName, string.Empty);
        File.WriteAllText(@charcfileName, string.Empty);
        File.WriteAllText(@inGameTextFileName, string.Empty);
        for (int i = 0; i < Scene.transform.childCount; i++)
        {
            GameObject page = Scene.transform.GetChild(i).gameObject;

            GameObject backgroundObj = page.transform.Find("background").gameObject;
            GameObject objects = page.transform.Find("Objects").gameObject;
            GameObject characters = page.transform.Find("Characters").gameObject;
           
            int pageIndex = i + 1;



            try
            {


                using (System.IO.StreamWriter file = new StreamWriter(@bgfileName, true, System.Text.Encoding.UTF8))
                {
                    //File.WriteAllText(@bgfileName, string.Empty);
                    if (bBackground == false)
                    {
                        
                        file.WriteLine("PageID" + ',' + "Background" + ',' + "BGM");
                        bBackground = true;
                    }
                    string backgroundName = backgroundObj.transform.GetComponent<SpriteRenderer>().sprite.name;
                    string musicName = pageMusicList[i];
                    file.WriteLine(pageIndex.ToString() + ',' + backgroundName + ',' + musicName);
                    
                }
               
                using (System.IO.StreamWriter file = new StreamWriter(@objfileName, true, System.Text.Encoding.UTF8))
                {
                    //File.WriteAllText(@objfileName, string.Empty);
                    if (bObject == false)
                    {
                        file.WriteLine("PageID" + ',' + "ObjectName" + ',' + "SpriteName" + ',' + "Pos_X" + ',' + "Pos_Y" + ',' + "Pos_Z" + ',' + "Scale" + ','+ "RotationY" + ',' + "RotationZ" + ',' + "SortOrder");
                        bObject =true;
                    }

                    for (int j = 0; j < objects.transform.childCount; j++)
                    {
                        Transform obj = objects.transform.GetChild(j);
                        string objName = obj.name;
                        string spriteName = obj.GetComponent<SpriteRenderer>().sprite.name;
                        float pos_x = obj.transform.localPosition.x;
                        float pos_y = obj.transform.localPosition.y;
                        float pos_z = obj.transform.localPosition.z;
                        float scale = obj.transform.localScale.x;
                        float rotationY = obj.transform.rotation.y;
                        float rotationZ = obj.transform.eulerAngles.z;
                        int order = obj.transform.GetComponent<SpriteRenderer>().sortingOrder;
                        file.WriteLine(pageIndex.ToString() + ',' + objName + ',' + spriteName + ',' + pos_x + ',' + pos_y + ',' + pos_z + ',' + scale + ',' + rotationY + ',' + rotationZ + ',' + order);
                    }


                }
               
                using (System.IO.StreamWriter file = new StreamWriter(@charcfileName, true, System.Text.Encoding.UTF8))
                {
                    //File.WriteAllText(@charcfileName, string.Empty);
                    if (bCharacter == false)
                    {
                        file.WriteLine("PageID" + ',' + "CharacterName" + ',' + "Pos_X" + ',' + "Pos_Y" + ',' + "Pos_Z" + ',' + "Scale" + ',' + "Hat" + ',' + "Head" + ',' + "Body" + ',' + "Leg" + ',' + "Face" + ',' + "RotationY" + ',' + "RotationZ" + ',' + "SortOrder");
                        bCharacter = true;
                    }

                    for (int k = 0; k < characters.transform.childCount; k++)
                    {
                        Transform obj = characters.transform.GetChild(k);
                        string objName = obj.name;
                        float pos_x = obj.transform.localPosition.x;
                        float pos_y = obj.transform.localPosition.y;
                        float pos_z = obj.transform.localPosition.z;
                        float scale = obj.transform.localScale.x;
                        float rotationY = obj.transform.rotation.y;
                        float rotationZ = obj.transform.eulerAngles.z;
                        int order = obj.transform.GetComponent<SpriteRenderer>().sortingOrder;


                        string hat = "none", head = "none", body = "none", legs = "none", face = "none";

                        if (obj.transform.Find("Hat").GetComponent<SpriteRenderer>().sprite != null) hat = obj.transform.Find("Hat").GetComponent<SpriteRenderer>().sprite.name;
                        if (obj.transform.Find("Head").GetComponent<SpriteRenderer>().sprite != null) head = obj.transform.Find("Head").GetComponent<SpriteRenderer>().sprite.name;
                        if (obj.transform.Find("Body").GetComponent<SpriteRenderer>().sprite != null) body = obj.transform.Find("Body").GetComponent<SpriteRenderer>().sprite.name;
                        if (obj.transform.Find("Legs").GetComponent<SpriteRenderer>().sprite != null) legs = obj.transform.Find("Legs").GetComponent<SpriteRenderer>().sprite.name;
                        if (obj.transform.Find("Face").GetComponent<SpriteRenderer>().sprite != null) face = obj.transform.Find("Face").GetComponent<SpriteRenderer>().sprite.name;
                        file.WriteLine(pageIndex.ToString() + ',' + objName + ',' + pos_x + ',' + pos_y + ',' + pos_z + ',' + scale + ','   + hat + ',' + head + ',' + body + ',' + legs + ',' + face + ',' + rotationY.ToString() + ',' + rotationZ.ToString() + ',' + order);
                    }


                }

                
            }
            catch (Exception ex)
            {

                throw new ApplicationException("error:", ex);

            }
        }


        try
        {
            
            using (System.IO.StreamWriter file = new StreamWriter(@inGameTextFileName, true, System.Text.Encoding.UTF8))
            {
                //File.WriteAllText(@charcfileName, string.Empty);
                if (btext == false)
                {
                    file.WriteLine("PageID" + ',' + "TextID" + ',' + "OriginText");
                    btext = true;
                }

                for (int k = 0; k < sceneTextList.Count; k++)
                {
                    for (int j = 0; j < sceneTextList[k].Count; j++)
                    {

                        int pageID = k + 1;
                        int textID = j + 1;
                        string text = sceneTextList[k][j];

                        string replace=text.Replace(",", "/");


                        //File.AppendAllText(@inGameTextFileName, pageID.ToString() + ',' + textID.ToString() + ',' + replace + Environment.NewLine, System.Text.Encoding.UTF8);

                        file.WriteLine(pageID.ToString() + ',' + textID.ToString() + ',' + replace);

                    }
                }
            }


        }
        catch (Exception ex)
        {

            throw new ApplicationException("error:", ex);

        }



    }
    public void ClearPage(GameObject p)
    {
        Transform objects = p.transform.Find("Objects");
        Transform characters = p.transform.Find("Characters");

        for (int i = 0; i < objects.childCount; i++)
        {
            Destroy(objects.GetChild(i).gameObject);
        }

        for (int i = 0; i < characters.childCount; i++)
        {
            Destroy(characters.GetChild(i).gameObject);
        }
    }

  
    public void NewPage()
    {
        
        GameObject page=Instantiate(Tpage, GameObject.Find("Scene").transform);
        pageList.Add(page);
        page.name = "Page_" + pageList.Count;
        ClearPage(page);

        GameObject pageImage = Instantiate(TpageImage, GameObject.Find("PageImageContainer").transform);
        pageImage.name = "Page_" + pageList.Count;
        ShowPageImageList();
        ShowPage(page.name);

        List<string> textList = new List<string>();
        textList.Add("");
        sceneTextList.Add(textList);
        

        pageMusicList.Add("");

        

    }


    public void DestroyPageImage(string name)
    {
        int deletePageIndex = int.Parse(name.Substring(name.Length - 1));
        GameObject deletePage=GameObject.Find("Scene").transform.Find(name).gameObject;
        pageList.Remove(deletePage);
        Destroy(deletePage);

        
        pageMusicList.RemoveAt(deletePageIndex - 1);
        GameObject lastPage = pageList[pageList.Count - 1];
        ShowPage(lastPage.name);
        ShowPageImageList();

        List<string> deleteTextList = sceneTextList[deletePageIndex-1];
        sceneTextList.Remove(deleteTextList);
        deleteTextList.Clear();


       
       
       








    }

    public void GoToEntrance()
    {


        Debug.Log(GameObject.Find("SoundManager").GetComponent<AudioSource>().isPlaying);

        if(GameObject.Find("SoundManager").GetComponent<AudioSource>().isPlaying==true)
        {
            GameObject.Find("SoundManager").GetComponent<AudioSource>().Stop();
            Debug.Log(GameObject.Find("SoundManager").GetComponent<AudioSource>().isPlaying);
        }

      
        SceneManager.LoadScene("Entrance");
    }


    public void ShowPage(string pageName)
    {
       

        foreach (GameObject g in pageList)
        {

            if(g.name!=pageName)
            {

                g.SetActive(false);
            }

            else
            {

                g.SetActive(true);
                

            }
        }
    }
  

    public void ShowPageImageList()
    {

        int pageCount = EditManager.GetEditManager().pageList.Count;

        Transform pageImageList = GameObject.Find("PageImageList").transform.Find("PageImageContainer");
        TpageImage = Resources.Load<GameObject>("Prefabs/PageImagePrefab");
       
        foreach (Transform x in pageImageList)
            Destroy(x.gameObject);

        for (int i = 0; i < pageCount; i++)
        {
            GameObject image = Instantiate(TpageImage, pageImageList);
            image.name = "Page_" + (i + 1).ToString();
            Transform backgroudObj = EditManager.GetEditManager().pageList[i].transform.Find("background");
            Sprite sprite = backgroudObj.GetComponent<SpriteRenderer>().sprite;
            image.GetComponent<Image>().sprite = sprite;

        }

        
    }

    public void RefreshPagesID()
    {
        GameObject scene = GameObject.Find("Scene");

        //Debug.Log(scene.transform.childCount);
        for (int i = 0; i < scene.transform.childCount; i++)
        {

            int index = i + 1;
            scene.transform.GetChild(i).name = "Page_" + index.ToString();


            if (scene.transform.GetChild(i).gameObject.activeSelf==true)
            {


                curPageIndex = int.Parse(scene.transform.GetChild(i).name.Substring(scene.transform.GetChild(i).name.Length - 1));

               
            }

        }

    }


    MiniGame SetUpMiniGame()
    {
        MiniGame game = new MiniGame();

        GameObject scene = GameObject.Find("Scene");

        for (int i = 0; i < scene.transform.childCount; i++)
        {
            Transform curPage = scene.transform.GetChild(i);
            Page page = new Page();
            page.background = curPage.transform.Find("background").GetComponent<SpriteRenderer>().sprite.name;
            game.pages.Add(page);
        }
        return game;
    }
    

    public void AddTextCreator()
    {

        RectTransform rt = textCreatorList.transform.parent.GetComponent<RectTransform>();
        float height = rt.rect.height;
        rt.sizeDelta = new Vector2(0, height+200);
        GameObject t=Instantiate(Ttextcreator,textCreatorList.transform);
        t.transform.Find("TextID").GetComponent<Text>().text = textCreatorList.transform.childCount.ToString() + ".";
       
       
    }

    void RefreshTextList()
    {
        if (textCreatorList.activeSelf==true)
        {
            for (int i = 0; i < textCreatorList.transform.childCount; i++)
            {
                textCreatorList.transform.GetChild(i).name = "TextCreator_" + (i + 1);
                textCreatorList.transform.GetChild(i).Find("TextID").GetComponent<Text>().text = (i + 1).ToString();

            }
        }

    }

    public void SavePageText()
    {
       
        for (int i = 0; i < textCreatorList.transform.childCount; i++)
        {
            string text = textCreatorList.transform.GetChild(i).Find("TextCreatorInput").GetComponent<InputField>().text;

            if (text.Trim() == "")
            {
                emptyTextPanel.SetActive(true);

                return;

            }
            
           
        }
        int pageIndex = curPageIndex;
        sceneTextList[pageIndex - 1].Clear();
        for (int i = 0; i < textCreatorList.transform.childCount; i++)
        {
            string text = textCreatorList.transform.GetChild(i).Find("TextCreatorInput").GetComponent<InputField>().text;

            sceneTextList[pageIndex - 1].Add(text);
        }

        if (GameObject.Find("TitleInput").GetComponent<InputField>().text.Trim() != "")
            gameTitle = GameObject.Find("TitleInput").GetComponent<InputField>().text;
        else
            gameTitle = "UnNameGame";

        Debug.Log(gameTitle);

        GameObject.Find("TextViewer").SetActive(false);
    }

    public void LoadPageText()
    {
        GameObject.Find("TextListScollBar").GetComponent<Scrollbar>().value = 1;
        for (int i = 0; i < textCreatorList.transform.childCount; i++)
        {
            Destroy(textCreatorList.transform.GetChild(i).gameObject);
        }

        int pageIndex = curPageIndex;


        if(sceneTextList[pageIndex - 1].Count==0)
            Instantiate(Ttextcreator, textCreatorList.transform);

        for (int i = 0; i < sceneTextList[pageIndex - 1].Count; i++)
        {
            RectTransform rt = textCreatorList.transform.parent.GetComponent<RectTransform>();
            float height = rt.rect.height;
            rt.sizeDelta = new Vector2(0, height + 200);
            GameObject t = Instantiate(Ttextcreator, textCreatorList.transform);
          
           
            t.transform.Find("TextCreatorInput").GetComponent<InputField>().text = sceneTextList[pageIndex - 1][i];
        }


       
    }

    private void AudioPlay()
    {


        if (curPageIndex - 1 <= pageMusicList.Count - 1)
        {
            if (pageMusicList[curPageIndex - 1] != "")
            {
                curPageMusicName = pageMusicList[curPageIndex - 1];

                AudioSource audio = GameObject.Find("SoundManager").GetComponent<AudioSource>();
                AudioClip clip = Resources.Load<AudioClip>("Audio/" + pageMusicList[curPageIndex - 1]);

                if(!clip)
                {

                    clip = tempMusicList[curPageMusicName];

                }


                audio.clip = clip;

                if (audio.isPlaying == false)
                    audio.Play();

                //Debug.Log("PageIndex: " + curPageIndex.ToString() + " " + curPageMusicName);


            }

            else
            {
                AudioSource audio = GameObject.Find("SoundManager").GetComponent<AudioSource>();

                if (audio.isPlaying == true)
                    audio.Stop();



            }
        }
    }


    public void OpenImageFileBrowser()
    {

        StartCoroutine( ShowLoadBGDialogCoroutine());
    }



    public void OpenObjImageFileBrowser()
    {
        StartCoroutine(ShowLoadObjDialogCoroutine());

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



    public void OpenMusicFileBrowser()
    {
        StartCoroutine(
          ShowLoadMusicDialogCoroutine());
    }

    private IEnumerator LoadAuido(string audiopath)
    {


        string audioName = audiopath.Split('\\')[audiopath.Split('\\').Length - 1];


       
        WWW request = GetAudioFromFile(audiopath);
        yield return request;

        AudioClip audioClip = NAudioPlayer.FromMp3Data(request.bytes);

        
        audioClip.name = audioName;


        if (!tempMusicList.ContainsKey(audioName))
        {

            tempMusicList.Add(audioName, audioClip);
        }


        pageMusicList[curPageIndex - 1] = audioClip.name;

        if(GameObject.Find("SoundManager").GetComponent<AudioSource>().enabled==false)
        {

            GameObject.Find("SoundManager").GetComponent<AudioSource>().enabled = true;
        }
        //GameObject.Find("SoundManager").GetComponent<AudioSource>().clip = audioClip;
        //GameObject.Find("SoundManager").GetComponent<AudioSource>().Play();





    }

    private WWW GetAudioFromFile(string audiopath)
    {
        WWW reqeust = new WWW(audiopath);
        return reqeust;
    }

    public void fileBrowser()
    {
        StartCoroutine(
        ShowLoadBGDialogCoroutine());
    }



    IEnumerator ShowLoadBGDialogCoroutine()
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("Images", ".jpg", ".jpeg", ".png"));

        //FileBrowser.SetDefaultFilter(".jpg");
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");

        string path = FileBrowser.Result;
       
        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)

        if (path != "")
        {
            string fileName = path.Split('\\')[path.Split('\\').Length - 1];

            byte[] fileData = getImageByte(path); // ERROR: The name 'File' does not exist in the current context?
            Texture2D t2d = new Texture2D(2, 2);
            //根据路劲读取字节流再转换成图片形式
            t2d.LoadImage(fileData);


            Sprite sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f));
            // sprite.name = fileName.Split('.')[0];

            sprite.name = fileName;
            GameObject.Find("background").GetComponent<SpriteRenderer>().sprite = sprite;
            GameObject.Find("background").GetComponent<FullScreenSprite>().Fit();



            if (!Directory.Exists(System.Environment.CurrentDirectory + "/GameResources/" + "GameImage"))
            {
                Directory.CreateDirectory(System.Environment.CurrentDirectory + "/GameResources/" + "GameImage");
            }

            string ExternalImageFilePath = System.Environment.CurrentDirectory + "/GameResources/" + "GameImage";
            string igfileName = ExternalImageFilePath + "/" + fileName;

            if (!File.Exists(igfileName))
            {
                System.IO.File.Copy(path, igfileName);
            }
        }

       // Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);
    }

    IEnumerator ShowLoadObjDialogCoroutine()
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("Images", ".png"));
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");

        string path = FileBrowser.Result;

        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)

        if (path != "")
        {
            string fileName = path.Split('\\')[path.Split('\\').Length - 1];

            byte[] fileData = getImageByte(path); // ERROR: The name 'File' does not exist in the current context?
            Texture2D t2d = new Texture2D(2, 2);
            //根据路劲读取字节流再转换成图片形式
            t2d.LoadImage(fileData);


            Sprite sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f));
            //sprite.name = fileName.Split('.')[0];
            sprite.name = fileName;

            GameObject obj = InstantiateSprite(sprite);


            if (!Directory.Exists(System.Environment.CurrentDirectory + "/GameResources/" + "GameImage"))
            {
                Directory.CreateDirectory(System.Environment.CurrentDirectory + "/GameResources/" + "GameImage");
            }

            string ExternalImageFilePath = System.Environment.CurrentDirectory + "/GameResources/" + "GameImage";
            string igfileName = ExternalImageFilePath + "/" + fileName;

            if (!File.Exists(igfileName))
            {
                System.IO.File.Copy(path, igfileName);
            }
        }

        // Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);
    }

    IEnumerator ShowLoadMusicDialogCoroutine()
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("Music", ".mp3", ".wav"));
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");

        string path = FileBrowser.Result;


       
        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)

        string audioName = path.Split('\\')[path.Split('\\').Length - 1];



        if (path != "")
        {
            Debug.Log(path);

            if (!Directory.Exists(System.Environment.CurrentDirectory + "/GameResources/" + "GameMusic"))
            {
                Directory.CreateDirectory(System.Environment.CurrentDirectory + "/GameResources/" + "GameMusic");
            }

            string ExternalImageFilePath = System.Environment.CurrentDirectory + "/GameResources/" + "GameMusic";
            string igfileName = ExternalImageFilePath + "/" + audioName;

            if (!File.Exists(igfileName))
            {
                System.IO.File.Copy(path, igfileName);
            }

            string formatPath = string.Format("file://{0}", path);


            StartCoroutine(LoadAuido(formatPath));


        }

        // Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);
    }



    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");

        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)



        Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);
    }


    public void OpenEXE(string _exePathName, string _exeArgus)
    {
        try
        {
            System.Diagnostics.Process myprocess = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(_exePathName, _exeArgus);
            myprocess.StartInfo = startInfo;
            myprocess.StartInfo.UseShellExecute = false;
            myprocess.Start();
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("出错原因：" + ex.Message);
        }
    }

    public void OpenGame()
    {


        System.Diagnostics.Process.Start(@"C:\Users\ZHENG YANG\Desktop\New folder (2)\FINAL_SP\Windows_SP\MiniGame_SP.exe");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
