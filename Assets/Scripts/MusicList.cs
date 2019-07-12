using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class MusicList : MonoBehaviour
{
    public AudioClip Sound;
    static public string BGMName = "";
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
        SoundManager.instance.GetComponent<AudioSource>().clip = Sound;
        SaveBGMName();
    }

    void SaveBGMName()
    {
        if (this.name != "None")
            BGMName = this.name;

        // test code. * Get current page from editor manager instead.
        // --------------------------------------------
        int currentPage = 0;

        if (PlayerPrefs.HasKey(currentPage.ToString()+"BGM"))
            PlayerPrefs.DeleteKey("BGM");

        PlayerPrefs.SetString("BGM", this.name);

        PlayerPrefs.Save();

    }

}
