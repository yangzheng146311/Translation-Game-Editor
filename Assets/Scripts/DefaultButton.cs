using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
