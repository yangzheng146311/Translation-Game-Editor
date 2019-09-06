using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class JsonIO : MonoBehaviour
{


    public SceneCharacter character;
    public SceneObject obj;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void SaveGame(MiniGame game)
    {
       
        SaveGameBackGround(game);
        SaveGameObject(game);
        



    }

    private static void SaveGameObject(MiniGame game)
    {
        string fileName = game.name + "_Objects.txt";



    }

    private static void SaveGameBackGround(MiniGame game)
    {
        string fileName = game.name + "_Background.txt";
        try
        {
            using (System.IO.StreamWriter file = new StreamWriter(@fileName, true))
            {

                file.WriteLine("PageID", "Background");




                for (int i = 0; i < game.pages.Count; i++)
                {
                    file.WriteLine((i + 1).ToString(), game.pages[i].background);

                }
            }
            


        }


        catch(Exception ex)
        {

            throw new ApplicationException("error:" , ex);

        }


        
    }
}
