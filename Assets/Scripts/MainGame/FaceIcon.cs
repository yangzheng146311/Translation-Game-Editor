using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceIcon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Debug.Log(gameObject.name);
        }
    }

    public void ChangeFace()
    {

        //Debug.Log(gameObject.name);
        Sprite sp = Resources.Load<Sprite>("Art assets/Characters art/Expression/Expressions/" + gameObject.name);
        GameObject.Find("CharacterUI").transform.Find("character").Find("Face").GetComponent<SpriteRenderer>().sprite = sp;
    }
}
