﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BG_Switch : MonoBehaviour
{
    private GameObject bg ;
    private Sprite background;
    // Start is called before the first frame update
    void Start()
    {
        bg = GameObject.Find("background");
        background = Resources.Load<Sprite>("Art assets/Background art/" + this.name);
        this.GetComponent<Image>().sprite = background;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackGroundSwitch()
    {
        Debug.Log(this.name);


        bg.GetComponent<SpriteRenderer>().sprite = background;
    }
}
