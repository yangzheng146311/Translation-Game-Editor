using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditManager : MonoBehaviour
{

    static public EditManager editManager;
    private Dictionary<string, Sprite> spriteDic;
    public Dictionary<string, GameObject> sceneObjects;
    private Dictionary<string, int> objTypes;
    
    // Start is called before the first frame update
    void Start()
    {
        
        sceneObjects = new Dictionary<string, GameObject>();
        objTypes = new Dictionary<string, int>();
        spriteDic = new Dictionary<string, Sprite>();
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

    }
    public void ClearPage()
    {

    }


}
