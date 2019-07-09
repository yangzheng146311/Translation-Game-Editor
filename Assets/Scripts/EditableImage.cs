using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableImage : MonoBehaviour
{
    private float startPosX;
    private float startPosY;

    Vector3 originPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("item:" + transform.localPosition);
    }

    public void OnImageClick()
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = (Camera.main.ScreenToWorldPoint(mousePos));
        

        
        startPosX = mousePos.x - this.transform.GetComponent<RectTransform>().position.x;
       startPosY = mousePos.y - this.transform.GetComponent<RectTransform>().position.y;



    }

    public void OnImageDrag()
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition ;
        mousePos = (Camera.main.ScreenToWorldPoint(mousePos));
       

    


      

        this.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0)*50;


    }
    


}
