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

        if (this.gameObject.name != "CS_GAME" && this.gameObject.name != "CT_GAME" && this.gameObject.name != "EN_GAME" && this.gameObject.name != "SP_GAME" && this.gameObject.name != "FR_GAME")
        {

            string gameName = this.gameObject.name;
            MenuEvent.loadingGameName = gameName;
            SceneManager.LoadScene("DemoGame");
        }
        else
        {

            Debug.Log(System.Environment.CurrentDirectory+"\\"+this.gameObject.name + "\\" +this.gameObject.name+".exe");
            System.Diagnostics.Process.Start(System.Environment.CurrentDirectory + "\\" + this.gameObject.name + "\\" + this.gameObject.name + ".exe");
        }



    }


    public void DeleteGame()
    {

        if (this.gameObject.name != "CS_GAME" && this.gameObject.name != "CT_GAME" && this.gameObject.name != "EN_GAME" && this.gameObject.name != "SP_GAME" && this.gameObject.name != "FR_GAME")
        {



            string gameName = this.gameObject.name;
            string path =System.Environment.CurrentDirectory + "/GameData/" + gameName;
            DirectoryInfo dir = new DirectoryInfo(path);
            dir.Delete(true);
        }
    }
}
