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


     
        loadingGameName = "";

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

    public void LoadTeacherGameList()
    {


        for (int i = 0; i < TeacherGameList.transform.childCount; i++)
        {
            if (TeacherGameList.GetChild(i).name != "Image")
                Destroy(TeacherGameList.GetChild(i).gameObject);
        }

        string path = System.Environment.CurrentDirectory + "/GameData";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string[] GameFolders = Directory.GetDirectories(System.Environment.CurrentDirectory + @"\GameData");

        foreach (var s in GameFolders)
        {

            string fileName = s;
            GameObject gameImage = Instantiate(teacherGameImage, TeacherGameList.transform);

            Debug.Log(s);
            gameImage.name = s.Split('\\')[6];
            gameImage.transform.Find("Text").GetComponent<Text>().text = gameImage.name;
            gameImage.gameObject.SetActive(true);
        }


        LoadInBuildGameImage_Teacher();
}
    public void LoadStudentGameList()
    {
        for (int i = 0; i < StudentGameList.transform.childCount; i++)
        {
            if (StudentGameList.GetChild(i).name != "Image")
                Destroy(StudentGameList.GetChild(i).gameObject);
        }

        string path = System.Environment.CurrentDirectory + "/GameData";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string[] GameFolders = Directory.GetDirectories(System.Environment.CurrentDirectory + @"\GameData");
        foreach (var s in GameFolders)
        {
           // Debug.Log(s);

            string fileName = s;
            GameObject gameImage = Instantiate(studentGameImage, StudentGameList.transform);
            gameImage.name = s.Split('\\')[6];

            gameImage.transform.Find("Text").GetComponent<Text>().text = gameImage.name;

            gameImage.gameObject.SetActive(true);


        }

        LoadInBuildGameImage_Student();
    }


    private void LoadInBuildGameImage_Student()
    {
        string[] fileNames = { "CS_GAME", "CT_GAME", "SP_GAME", "EN_GAME" };



        foreach (string s in fileNames)
        {
            GameObject gameImage = Instantiate(studentGameImage, StudentGameList.transform);
            gameImage.name = s;

            gameImage.transform.Find("Text").GetComponent<Text>().text = gameImage.name;

            gameImage.gameObject.SetActive(true);
        }


    }

    private void LoadInBuildGameImage_Teacher()
    {
        string[] fileNames = { "CS_GAME", "CT_GAME", "SP_GAME", "EN_GAME" };



        foreach (string s in fileNames)
        {
            GameObject gameImage = Instantiate(teacherGameImage, TeacherGameList.transform);
            gameImage.name = s;

            gameImage.transform.Find("Text").GetComponent<Text>().text = gameImage.name;

            gameImage.gameObject.SetActive(true);
        }


    }







}
