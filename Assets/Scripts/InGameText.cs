using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InGameText : MonoBehaviour
{
   
    public Text idText;
    public Text inputText;
    public static InputField t;
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
        t = GetComponent<InputField>();
        t.text="test dlksfhljdshl jdhs";
        t.placeholder.GetComponent<Text>().text = t.text;
       // inputText.text = t.text;
    }

    void ReadText()
    {
        int line = 1;
        StreamReader streamReader = new StreamReader("Assets/GameLoad/test.csv");

        bool EndOfFile = false;
        while (!EndOfFile)
        {

            string data_string = streamReader.ReadLine();
            if (data_string == null)
            {

                EndOfFile = true;
                break;

            }

            if (line != 1)
            {
                string[] data_value = data_string.Split(',');

                int SceneID = int.Parse(data_value[0]);
                int PageID = int.Parse(data_value[2]);
                string diaglogueText = data_value[3];

            }
            line++;

        }
    }

}
