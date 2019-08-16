using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuEvent : MonoBehaviour
{
    public Transform StudentGameList;
    public Transform TeacherGameList;
    public GameObject studentGameImage;
    public GameObject teacherGameImage;

    public static string loadingGameName;

    string[] GameFolders;

    private void Awake()
    {


        LoadTeacherGameList();
        LoadStudentGameList();

        loadingGameName = "";

    }


  
    public void LoadTeacherGameList()
    {
        string path = Application.persistentDataPath + "/GameData";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string[] GameFolders = Directory.GetDirectories(Application.persistentDataPath + "/GameData");

        foreach (var s in GameFolders)
        {

            string fileName = s;
            GameObject gameImage = Instantiate(teacherGameImage, TeacherGameList.transform);
            gameImage.name = s.Split('/')[7].Split('\\')[1];
            gameImage.transform.Find("Text").GetComponent<Text>().text = gameImage.name;




        }
    }

    public void LoadStudentGameList()
    {
        Debug.Log(Application.persistentDataPath + "/GameData");
        string path = Application.persistentDataPath + "/GameData";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string[] GameFolders = Directory.GetDirectories(Application.persistentDataPath + "/GameData");
        foreach (var s in GameFolders)
        {

            string fileName = s;
            GameObject gameImage = Instantiate(studentGameImage, StudentGameList.transform);
            gameImage.name = s.Split('/')[7].Split('\\')[1];

            gameImage.transform.Find("Text").GetComponent<Text>().text = gameImage.name;


        }
    }


    // Start is called before the first frame update
    void Start()
    {
        studentGameImage.SetActive(false);
        teacherGameImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToEditor()
    {


        SceneManager.LoadScene("MainScene");
    }
     

    public void ExitTheGame()
    {

        Application.Quit();

    }



    
}
