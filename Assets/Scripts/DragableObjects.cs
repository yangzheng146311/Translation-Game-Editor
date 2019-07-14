using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObjects : MonoBehaviour
{

    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

   

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }

        
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.position.x;
            startPosY = mousePos.y - this.transform.position.y;
            isBeingHeld = true;


        }

        if (Input.GetMouseButtonDown(1))
        {
            
            EditManager.GetEditManager().DestroySprite(this.name);

        }



    }
    
    




    private void OnMouseUp()
    {
        isBeingHeld = false;

    }
}
