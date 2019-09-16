﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObjects : MonoBehaviour
{

    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

    Vector3 scale;
    float offset = 0.2f;
    float maxSize = 3.0f;
    float minSize = 0.2f;



    private void Start()
    {
        scale = this.transform.localScale;
    }

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

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (scale.x <= maxSize)
            {
                scale.x += offset;
                scale.y += offset;
                scale.z += offset;
                this.transform.localScale = scale;
            }

        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (scale.x > minSize)
            {
                scale.x -= offset;
                scale.y -= offset;
                scale.z -= offset;
                this.transform.localScale = scale;
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            
                transform.Rotate(new Vector3(0, 180, 0), Space.World);

        }

        

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    if(transform.rotation.y==0)
        //    transform.Rotate(new Vector3(0, 180, 0),Space.Self);

        //}

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //   // if (transform.rotation.y == 180)
        //        transform.Rotate(new Vector3(0, 0, 0), Space.Self);

        //}




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
