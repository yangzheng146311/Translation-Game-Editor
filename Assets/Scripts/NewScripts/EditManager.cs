using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class EditManager : MonoBehaviour
{

    static public EditManager editManager;
    private Dictionary<string, Sprite> spriteDic;
    public Dictionary<string, GameObject> sceneObjects;
    private Dictionary<string, int> objTypes;
    private GameObject Tpage;
    private GameObject TpageImage;
    public List<GameObject> pageList;

    public GameObject textCreatorList;
    GameObject Ttextcreator;
    List<List<string>> sceneTextList;
    int curPageIndex;
    
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
        sceneTextList.Add(textList);
       
        Ttextcreator = Resources.Load<GameObject>("Prefabs/TextCreator");
        


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

        RefreshPagesID();
        RefreshTextList();

        //Debug.Log("textListCount:"+sceneTextList.Count);
        //Debug.Log(curPageIndex);
        //Debug.Log("textCount:" + sceneTextList[curPageIndex-1].Count);

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

    public void DestroySprite(string name)
    {
        Destroy(sceneObjects[name]);
        sceneObjects.Remove(name);
        int index = name.IndexOf('_');
        
        if(objTypes[name.Substring(0, index)]>0)
        objTypes[name.Substring(0,index)]--;


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
    public void SavePage()
    {
        Debug.Log("SavePage");
        string bgfileName = "Assets/GameLoad/_Background.txt";
        string objfileName = "Assets/GameLoad/_Obj.txt";
        string charcfileName = "Assets/GameLoad/_Character.txt";
        string inGameTextFileName= "Assets/GameLoad/_ingametext.txt";

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
                using (System.IO.StreamWriter file = new StreamWriter(@bgfileName, true))
                {
                    //File.WriteAllText(@bgfileName, string.Empty);
                    if (bBackground == false)
                    {
                        
                        file.WriteLine("PageID" + ',' + "Background");
                        bBackground = true;
                    }
                    string backgroundName = backgroundObj.transform.GetComponent<SpriteRenderer>().sprite.name;
                    file.WriteLine(pageIndex.ToString() + ',' + backgroundName);
                    
                }

                using (System.IO.StreamWriter file = new StreamWriter(@objfileName, true))
                {
                    //File.WriteAllText(@objfileName, string.Empty);
                    if (bObject == false)
                    {
                        file.WriteLine("PageID" + ',' + "ObjectName" + ',' + "SpriteName" + ',' + "Pos_X" + ',' + "Pos_Y" + ',' + "Scale");
                        bObject =true;
                    }

                    for (int j = 0; j < objects.transform.childCount; j++)
                    {
                        Transform obj = objects.transform.GetChild(j);
                        string objName = obj.name;
                        string spriteName = obj.GetComponent<SpriteRenderer>().sprite.name;
                        float pos_x = obj.transform.position.x;
                        float pos_y = obj.transform.position.y;
                        float scale = obj.transform.localScale.x;
                        file.WriteLine(pageIndex.ToString() + ',' + objName + ',' + spriteName + ',' + pos_x + ',' + pos_y + ',' + scale);
                    }


                }

                using (System.IO.StreamWriter file = new StreamWriter(@charcfileName, true))
                {
                    //File.WriteAllText(@charcfileName, string.Empty);
                    if (bCharacter == false)
                    {
                        file.WriteLine("PageID" + ',' + "CharacterName" + ',' + "Pos_X" + ',' + "Pos_Y" + ',' + "Scale" + ',' + "Hat" + ',' + "Head" + ',' + "Body" + ',' + "Leg");
                        bCharacter = true;
                    }

                    for (int k = 0; k < characters.transform.childCount; k++)
                    {
                        Transform obj = characters.transform.GetChild(k);
                        string objName = obj.name;
                        float pos_x = obj.transform.position.x;
                        float pos_y = obj.transform.position.y;
                        float scale = obj.transform.localScale.x;

                        string hat = "none", head = "none", body = "none", legs = "none";

                        if (obj.transform.Find("Hat").GetComponent<SpriteRenderer>().sprite != null) hat = obj.transform.Find("Hat").GetComponent<SpriteRenderer>().sprite.name;
                        if (obj.transform.Find("Head").GetComponent<SpriteRenderer>().sprite != null) head = obj.transform.Find("Head").GetComponent<SpriteRenderer>().sprite.name;
                        if (obj.transform.Find("Body").GetComponent<SpriteRenderer>().sprite != null) body = obj.transform.Find("Body").GetComponent<SpriteRenderer>().sprite.name;
                        if (obj.transform.Find("Legs").GetComponent<SpriteRenderer>().sprite != null) legs = obj.transform.Find("Legs").GetComponent<SpriteRenderer>().sprite.name;

                        file.WriteLine(pageIndex.ToString() + ',' + objName + ',' + pos_x + ',' + pos_y + ',' + scale + ',' + hat + ',' + head + ',' + body + ',' + legs);
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

            using (System.IO.StreamWriter file = new StreamWriter(@inGameTextFileName, true))
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
                        file.WriteLine(pageID.ToString() + ',' + textID.ToString() + ',' + text);

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

    public void LoadPage()
    {

        Debug.Log("LoadPage");

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
        sceneTextList.Add(textList);
        

    }


    public void DestroyPageImage(string name)
    {

        GameObject deletePage=GameObject.Find("Scene").transform.Find(name).gameObject;
        pageList.Remove(deletePage);
        Destroy(deletePage);
        
        GameObject lastPage = pageList[pageList.Count - 1];
        ShowPage(lastPage.name);
        ShowPageImageList();

        int deletePageIndex = int.Parse(name.Substring(name.Length - 1));
        List<string> deleteTextList = sceneTextList[deletePageIndex];
        sceneTextList.Remove(deleteTextList);
        deleteTextList.Clear();
        




    }




    public void ShowPage(string pageName)
    {

        foreach(GameObject g in pageList)
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
    public void NewScene()
    {

       // Scene s = SceneManager.GetSceneByName("GameScene");

        //SceneManager.UnloadScene(SceneManager.GetActiveScene());
       
   
        StartCoroutine(LoadYourAsyncScene());
        //SceneManager.SetActiveScene(s);
        //SceneManager.MoveGameObjectToScene(example, s);
        //Debug.Log(s.path);

        //Debug.Log(s.buildIndex);


    }

    IEnumerator LoadYourAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();
        GameObject example = Resources.Load<GameObject>("Example");
        GameObject obj = Instantiate(example);
        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);

        

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("GameScene"));
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
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
        int pageIndex = curPageIndex;
        sceneTextList[pageIndex - 1].Clear();
        for (int i = 0; i < textCreatorList.transform.childCount; i++)
        {
            string text = textCreatorList.transform.GetChild(i).Find("TextCreatorInput").GetComponent<InputField>().text;
            
            sceneTextList[pageIndex - 1].Add(text);
        }
    }

    public void LoadPageText()
    {
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
}
