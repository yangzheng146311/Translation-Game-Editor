using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameText : MonoBehaviour
{
   
    public Text idText;
    public Text inputText;
    public static Text t;
    // Start is called before the first frame update
    void Start()
    {
        InitText();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateText()
    {

    }

    void InitText()
    {
        t = GetComponent<Text>();
        t.text="test dlksfhljdshl jdhs";
        inputText.text = t.text;
    }

}
