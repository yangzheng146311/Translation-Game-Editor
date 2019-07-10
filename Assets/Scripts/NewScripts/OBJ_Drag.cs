using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OBJ_Drag : MonoBehaviour
{
    private Sprite obj_sprite;

    // Start is called before the first frame update
    void Start()
    {
        obj_sprite = Resources.Load<Sprite>("Art assets/Objects art(with board)/" + this.name);
        this.GetComponent<Image>().sprite = obj_sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateObject()
    {

        EditManager.GetEditManager().InstantiateSprite(this.name);
       
    }


    

}
