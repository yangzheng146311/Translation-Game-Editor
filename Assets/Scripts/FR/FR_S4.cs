using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FR_S4 : MonoBehaviour
{
    public GameObject inputframe;
    public GameObject title;
    public GameObject correctResult;
    public GameObject wrongResult;
    static public string sourceTitle;
    static public string transTitle;
    public string[] sourceText;
    public string[] translateText;
    public GameObject background;
    public GameObject instext;
    public GameObject sword;
    public GameObject Theseus;
    public GameObject weapons;
    public Sprite itemSword;



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
                        instext.GetComponent<Text>().text = inputtext;

                       

                        inputframe.gameObject.SetActive(false);
                        title.SetActive(false);

                        correctResult.SetActive(true);
                    }

                    else
                    {

                        wrongResult.SetActive(true);
                    }
                }

                if(actionIndex==3)
                {

                    if (inputtext.Equals(translateText[2]))
                    {
                        instext.GetComponent<Text>().text = inputtext;
                        inputframe.gameObject.SetActive(false);
                        correctResult.SetActive(true);
                        correctResult.transform.Find("Text").GetComponent<Text>().text = "Level Pass";
                    }
                    else
                    {

                        wrongResult.SetActive(true);
                    }
                }


                Debug.Log(actionIndex);

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
        instext.SetActive(true);
        Theseus.gameObject.SetActive(true);
        weapons.SetActive(true);
        sword.SetActive(true);
        inputframe.SetActive(true);
        inputframe.GetComponent<InputField>().text = "";


    }


    private void Action_2()
    {
        Debug.Log("Action_2");
        Theseus.GetComponent<Character_S4>().canMove = true;

    }


    private void Action_3()
    {
        Debug.Log("Action_3");
        instext.GetComponent<Text>().text = sourceText[dialogIndex];

        GameObject.Find("Weapons").transform.GetChild(0).GetComponent<Image>().sprite = itemSword;
        //correctResult.SetActive(true);


        //correctResult.transform.Find("Text").GetComponent<Text>().text = "Level Pass";

        inputframe.SetActive(true);
        inputframe.GetComponent<InputField>().text = "";

    }

    private void Action_4()
    {
        Debug.Log("Action_4");
        SceneManager.LoadScene("FR_S5");


    }




    public void NextAction()
    {

        

        actionIndex++;

        if(actionIndex!=3)
        dialogIndex++;

        actionList[actionIndex]();
    }

    public void ShowCorrectAnswer()
    {
        inputframe.GetComponent<InputField>().text = translateText[dialogIndex];


    }

    public void EndGame()
    {

        Debug.Log("quit");
        Application.Quit();
    }
}
