using UnityEngine;
using System.Collections;


public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.


    void Awake()
    {
        if (instance == null)
       
            instance = this;

        else if (instance != this)
             Destroy(gameObject);

       // DontDestroyOnLoad(gameObject);
    }


   
    public void PlaySingle(AudioClip clip)
    {

        if (clip == null)
        {
            efxSource.clip = null;
            efxSource.enabled = false;
            EditManager editManager1 = EditManager.GetEditManager();
            editManager1.pageMusicList[editManager1.curPageIndex - 1] = "";
            return;
        }

        else
            efxSource.enabled = true;

        efxSource.clip = clip;

        efxSource.Play();

        EditManager editManager = EditManager.GetEditManager();
        editManager.curPageMusicName = clip.name;


        editManager.pageMusicList[editManager.curPageIndex-1] = editManager.curPageMusicName;
       



    }


   
    public void RandomizeSfx(params AudioClip[] clips)
    {

        int randomIndex = Random.Range(0, clips.Length);

       
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

       
        efxSource.pitch = randomPitch;

       
        efxSource.clip = clips[randomIndex];

       
        efxSource.Play();
    }
}

