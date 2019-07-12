﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicList : MonoBehaviour
{
    public AudioClip Sound1;
    public AudioClip Sound2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SoundManager.instance.RandomizeSfx(Sound1, Sound2);
    }
}