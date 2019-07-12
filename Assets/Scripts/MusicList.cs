using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicList : MonoBehaviour
{
    public AudioClip Sound;

    // Start is called before the first frame update
    void Start()
    {
        Sound=Resources.Load<AudioClip>("Audio/" + this.name);
        transform.GetChild(1).GetComponent<Text>().text = this.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SoundManager.instance.PlaySingle(Sound);
    }
}
