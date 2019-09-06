using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FR_S2 : MonoBehaviour
{
    public GameObject inputframe;
    public GameObject subtitle;
    public GameObject correctResult;
    public GameObject wrongResult;
    public GameObject Aethra;
    public GameObject Theseus; 


    public string[] sourceText;
    public string[] translateText;
  

    private int dialogIndex = 0;
    private int actionIndex = 0;
    delegate void Action();

    Action action;
    List<Action> actionList;




  
    private void Awake()
    {


        actionList = new List<Action>();
        actionList.Add(new Action(Action_0));
        actionList.Add(new Action(Action_1));
        actionList[0]();



    }


    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        ReplaceTitle();



        
    }

    public void ShowDialog(string source)
    {

        inputframe.SetActive(true);
        subtitle.SetActive(true);
        subtitle.GetComponent<Text>().text = source;
        inputframe.GetComponent<InputField>().text = "";
    }

    public void ReplaceTitle()
    {

        if (inputframe.activeSelf == true)
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {

                string inputtext = inputframe.GetComponent<InputField>().text;


                //if user translate correctly
                if (inputtext.Equals(translateText[dialogIndex]))
                { 


                    subtitle.GetComponent<Text>().text = inputtext;
                    inputframe.SetActive(false);
                   
                    actionIndex++;
                    dialogIndex++;
                    actionList[actionIndex]();


                   

                }

                //if user translate wrong
                else
                {

                    wrongResult.SetActive(true);


                }
                



            }
        }
    }



    private void Action_0()
    {
        
        ShowDialog(sourceText[dialogIndex]);

    }

    private void Action_1()
    {
        Aethra.transform.Rotate(new Vector3(0, 180, 0));
        Aethra.transform.Translate(Vector3.right*5,Space.World);
        Theseus.transform.Translate(Vector3.right * 6, Space.World);

        if (correctResult.activeSelf == false)
            correctResult.SetActive(true);
    }

    public void Load_S3()
    {


        SceneManager.LoadScene("FR_S3");
    }

}
