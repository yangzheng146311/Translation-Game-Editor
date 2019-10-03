using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FR_S7 : MonoBehaviour
{
    public GameObject subtitle;
    public GameObject inputframe;
    public GameObject title;
    public GameObject correctResult;
    public GameObject wrongResult;
    static public string sourceTitle;
    static public string transTitle;
    public string[] sourceText;
    public string[] translateText;
    public GameObject background;
    
    public GameObject Theseus;

    public GameObject[] foxes;


    private int dialogIndex = 0;
    private int actionIndex = 0;
    delegate void Action();

    Action action;
    List<Action> actionList;

    bool bEndScene = false;


    private void Awake()
    {


        actionList = new List<Action>();
        actionList.Add(new Action(Action_0));
        actionList.Add(new Action(Action_1));
        actionList.Add(new Action(Action_2));
        actionList.Add(new Action(Action_3));
        actionList.Add(new Action(Action_4));

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

          if(ifAllFoxesDie()==true)
        {



            if(bEndScene==false)
            {


                Debug.Log("EndScene");
                bEndScene = true;
            }
        }



    }


    bool ifAllFoxesDie()
    { 


        foreach(var f in foxes)
        {

            if(f.gameObject.GetComponent<Enemy_S7>().curHP>0)
            {
                return false;

            }
        }

        return true;
    }



    public void ShowInputFrame()
    {

        inputframe.SetActive(true);
    }

    public void ReplaceTitle()
    {

        if(inputframe.activeSelf==true)
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {
                
                string inputtext = inputframe.GetComponent<InputField>().text;


                if (actionIndex == 0)
                {
                    if (inputtext.Equals(translateText[0]))
                    {
                        title.GetComponent<Text>().text = inputtext;

                        transTitle = inputtext;
                     

                        inputframe.gameObject.SetActive(false);
                        title.SetActive(false);

                        correctResult.SetActive(true);
                    }

                    else
                    {

                        wrongResult.SetActive(true);
                      
                      
                    }
                }

                if (actionIndex == 1)
                {
                    if (inputtext.Equals(translateText[1]))
                    {
                        title.GetComponent<Text>().text = inputtext;

                        subtitle.transform.Find("Text").GetComponent<Text>().text = inputtext;


                        inputframe.gameObject.SetActive(false);
                       

                        correctResult.SetActive(true);
                    }

                    else
                    {

                        wrongResult.SetActive(true);


                    }
                }




            }
        }
    }

    private void Action_0()
    {
        sourceTitle = title.GetComponent<Text>().text;

    }


    private void Action_1()
    {
        Debug.Log("Action_1");
        background.SetActive(true);
        
        Theseus.gameObject.SetActive(true);

        dialogIndex++;
        ShowDialog(sourceText[dialogIndex]);
        






    }


    private void Action_2()
    {
        Debug.Log("Action_2");
        inputframe.SetActive(false);


        foreach (var f in foxes)
        {

            if (f.gameObject.activeSelf==false)
            {
                f.gameObject.SetActive(true);

            }

            Theseus.GetComponent<Character_S6>().canMove = true;
        }


    }


    private void Action_3()
    {
       

    }

    private void Action_4()
    {


    }




    public void NextAction()
    {

        

        actionIndex++;

        actionList[actionIndex]();
    }


    public void ShowDialog(string source)
    {
        subtitle.transform.Find("Text").GetComponent<Text>().text = source;
        inputframe.SetActive(true);
        subtitle.SetActive(true);

        inputframe.GetComponent<InputField>().text = "";
    }

    public void EndGame()
    {

        Debug.Log("quit");
        Application.Quit();
    }
}
