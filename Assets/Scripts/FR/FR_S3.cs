using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FR_S3 : MonoBehaviour
{
    public GameObject inputframe;
    public GameObject subtitle;
    public GameObject correctResult;
    public GameObject wrongResult;
    public GameObject Aethra;
    public GameObject Theseus;
    public Sprite equieTheseus;


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
        actionList.Add(new Action(Action_2));
        actionList.Add(new Action(Action_3));
        actionList.Add(new Action(Action_4));
        actionList.Add(new Action(Action_5));
        actionList.Add(new Action(Action_6));
        actionList[actionIndex]();



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
        subtitle.transform.Find("Text").GetComponent<Text>().text = source;
        inputframe.SetActive(true);
        subtitle.SetActive(true);
      
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
                    

                    subtitle.transform.Find("Text").GetComponent<Text>().text = inputtext;
                    inputframe.SetActive(false);

                    if (actionIndex != 5)
                    {
                        correctResult.SetActive(true);
                        correctResult.transform.Find("Text").GetComponent<Text>().text = "Translation Correct";
                    }
                    else
                    {
                        correctResult.SetActive(true);
                        correctResult.transform.Find("Text").GetComponent<Text>().text = "Level Pass";


                    }
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
        Debug.Log("Action_" + actionIndex);
        ShowDialog(sourceText[0]);

    }

    private void Action_1()
    {

        Debug.Log("Action_" + actionIndex);
        ShowDialog(sourceText[dialogIndex]);
    }

    private void Action_2()
    {
        Debug.Log("Action_" + actionIndex);
        ShowDialog(sourceText[dialogIndex]);

    }

    private void Action_3()
    {
        Debug.Log("Action_" + actionIndex);

        Theseus.transform.Translate(new Vector3(1.7f, 0, 0), Space.World);
        GameObject.Find("big rock").transform.Translate(new Vector3(0, 2.4f, 0), Space.World);
        Theseus.GetComponent<SpriteRenderer>().sprite = equieTheseus;

        ShowDialog(sourceText[dialogIndex]);




    }



    private void Action_4()
    {

        GameObject.Find("big rock").gameObject.SetActive(false);
        Debug.Log("Action_" + actionIndex);
        ShowDialog(sourceText[dialogIndex]);

    }

    private void Action_5()
    {
        Debug.Log("Action_" + actionIndex);
        ShowDialog(sourceText[dialogIndex]);

    }

    private void Action_6()
    {
        Debug.Log("Action_" + actionIndex);
        Load_S4();

    }
    public void Load_S4()
    {

        Debug.Log("Load S4");
        SceneManager.LoadScene("FR_S4");
    }


    public void NextAction()
    {


       

            actionIndex++;
            dialogIndex++;
            actionList[actionIndex]();
        

    }
}
