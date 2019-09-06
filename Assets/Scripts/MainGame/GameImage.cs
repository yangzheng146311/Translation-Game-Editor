using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadGame()
    {
        string gameName = this.gameObject.name;

        

        MenuEvent.loadingGameName = gameName;


        SceneManager.LoadScene("DemoGame");



    }


    public void DeleteGame()
    {

        string gameName = this.gameObject.name;
        string path = Application.persistentDataPath + "/GameData/" + gameName;
        DirectoryInfo dir = new DirectoryInfo(path);
        dir.Delete(true);
    }
}
