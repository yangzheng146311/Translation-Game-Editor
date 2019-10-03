using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;       //Allows us to use Lists.
using System;

[SerializeField]
public class CharacterCreator : MonoBehaviour
{

    public Button currentButton;
    public static int head;
    public static int body;
    public static int legs;
    public static int hat;
    public static string ID;

    public GameObject[] headTiles;
    public GameObject[] bodyTiles;
    public GameObject[] legsTiles;
    public GameObject[] hatTiles;

    public GameObject Head;
    public GameObject Body;
    public GameObject Legs;
    public GameObject Hat;

   
    GameObject curEditCharacter;


    // Start is called before the first frame update
    void Start()
    {
        head = 0;
        body = 0;
        legs = 0;
        hat = 0;
        ID = "00000000";

      

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetButton()
    {
        currentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
    }


    // Get character info from button name
    // -----------------------------------

    public void GetHead()
    {
        head = (currentButton.name[12] - '0') * 10 + currentButton.name[13] - '0';
        SetHead();

    }
    public void GetBody()
    {
        body = (currentButton.name[12] - '0') * 10 + currentButton.name[13] - '0';
        SetBody();
    }
    public void GetLegs()
    {
        legs = (currentButton.name[12] - '0') * 10 + currentButton.name[13] - '0';
        SetLegs();
    }
    public void GetHat()
    {
        hat = (currentButton.name[12] - '0') * 10 + currentButton.name[13] - '0';
        SetHat();
    }

   


    // Instantiate characters from tiles
    // -------------------------------------------
    public void SetHead()
    {
        GameObject toInstantiate = headTiles[head];
        GameObject instance =
            Instantiate(toInstantiate, Head.transform.position, Quaternion.identity) as GameObject;
        Destroy(Head);
        //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
        instance.transform.SetParent(this.transform);
        Head = instance;
        Head.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
        instance.name = "Head";
        instance.GetComponent<SpriteRenderer>().sortingOrder = 1;

    }
    public void SetBody()
    {
        GameObject toInstantiate = bodyTiles[body];
        GameObject instance =
            Instantiate(toInstantiate, Body.transform.position, Quaternion.identity) as GameObject;
        Destroy(Body);
        //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
        instance.transform.SetParent(this.transform);
        Body = instance;
        Body.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
        instance.name = "Body";
        instance.GetComponent<SpriteRenderer>().sortingOrder = 2;
    }
    public void SetLegs()
    {
        GameObject toInstantiate = legsTiles[legs];
        GameObject instance =
            Instantiate(toInstantiate, Legs.transform.position, Quaternion.identity) as GameObject;
        Destroy(Legs);
        //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
        instance.transform.SetParent(this.transform);
        Legs = instance;
        Legs.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
        instance.name = "Legs";
        instance.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
    public void SetHat()
    {
        GameObject toInstantiate = hatTiles[hat];
        GameObject instance =
            Instantiate(toInstantiate, Hat.transform.position, Quaternion.identity) as GameObject;
        Destroy(Hat);
        //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
        instance.transform.SetParent(this.transform);
        Hat = instance;
        Hat.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
        instance.name = "Hat";

        instance.GetComponent<SpriteRenderer>().sortingOrder = 3;

    }



    // Save character info as string
    // -----------------------------

    public void create()
    {
        ID = head.ToString() + body.ToString() + legs.ToString() + hat.ToString();
    }


    // Character Data
    // --------------

    void loadInData()
    {
        if (PlayerPrefs.HasKey("ID"))
        ID = PlayerPrefs.GetString("ID");
        head = (ID[0] - '0') * 10 + ID[1] - '0';
        body = (ID[2] - '0') * 10 + ID[3] - '0';
        legs = (ID[4] - '0') * 10 + ID[5] - '0';
        hat = (ID[6] - '0') * 10 + ID[7] - '0';
    }
    public void saveData()
    {
       
        if (PlayerPrefs.HasKey("ID"))
            PlayerPrefs.DeleteKey("ID");
            
        PlayerPrefs.SetString("ID", ID);
        PlayerPrefs.Save();

    }
    public void clearData()
    {
        head = 0;
        body = 0;
        legs = 0;
        hat = 0;
        ID = "00000000";
   
        if (PlayerPrefs.HasKey("ID"))
            PlayerPrefs.DeleteKey("ID");

        PlayerPrefs.Save();

    }

    

}
