using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame
{
    public List<Page> pages;
    public string name;
    public MiniGame()
    {

        pages = new List<Page>();
        name = "sameple";
    }
}

public class Page
{
    public List<SceneObject> objects;
    public List<SceneCharacter> characters;
    public string background;
    
    

    public Page()
    {

        objects = new List<SceneObject>();
        characters = new List<SceneCharacter>();
        background = "sampleBackground";


    }
}



public class SceneCharacter
{
    Dictionary<string, SceneObject> bodyparts;
    public float pos_x;
    public float pos_y;
    public float scale;

    public SceneCharacter()
    {
        bodyparts = new Dictionary<string, SceneObject>();
        pos_x = 0;
        pos_y = 0;
        scale = 1;

    }


}

public class SceneObject
{

    public string spriteName;
    public float pos_x;
    public float pos_y;
    public float scale;

    public SceneObject()
    {
        spriteName = "";
        pos_x = 0;
        pos_y = 0;
        scale = 1;


    }

}
