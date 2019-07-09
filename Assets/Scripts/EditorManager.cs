using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdidtManager : MonoBehaviour
{
    static public string chosenObjectName="Dog";
    private GameObject curEditObject;
    public GameObject templateObject;
    Sprite[] objects;
    bool isCreated = false;

    private Dictionary<string, Sprite> loadedObjects;

    // Start is called before the first frame update
    void Start()
    {
        loadedObjects = new Dictionary<string, Sprite>();
        try
        {
            objects = Resources.LoadAll<Sprite>("Art assets/Objects art");

            foreach (Sprite sprite in objects)
            {

                loadedObjects.Add(sprite.name, sprite);
               

            }

            Debug.Log(objects.Length);
        }
        catch(System.Exception err)
        {

            throw err;

        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSceneDrag()
    {
        


    }
    
    public void OnSceneClick()
    {

        Debug.Log("click");
        if (isCreated == false)
        {
            curEditObject = Instantiate(templateObject, GameObject.Find("ScenePanel").transform);
            curEditObject.GetComponent<Image>().sprite = loadedObjects[chosenObjectName];
            isCreated = true;
        }

    }


}
