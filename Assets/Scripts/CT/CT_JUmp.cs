using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CT_JUmp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu()
    {

        SceneManager.LoadScene("CT_S1");

    }

    public void LoadGame()
    {

        
        SceneManager.LoadScene("CT_S2");

    }
}
