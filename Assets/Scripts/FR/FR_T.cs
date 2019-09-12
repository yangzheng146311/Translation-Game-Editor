using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FR_T : MonoBehaviour
{
    public GameObject inputframe;
    public GameObject subtitle;
    public GameObject correctResult;
    public GameObject wrongResult;
    public GameObject Theseus;
    public GameObject Periphetes;



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

        MoveAction(Periphetes, new Vector2(-1.62f,-1.8f), 0);


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





                    //if (actionIndex != 5)
                    //{
                    //    correctResult.SetActive(true);
                    //    correctResult.transform.Find("Text").GetComponent<Text>().text = "Translation Correct";
                    //}
                    //else
                    //{
                    //    correctResult.SetActive(true);
                    //    correctResult.transform.Find("Text").GetComponent<Text>().text = "Level Pass";


                    //}
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
        ShowDialog(sourceText[0]);

    }

    private void Action_1()
    {


    }

    private void Action_2()
    {


    }

    private void Action_3()
    {





    }



    private void Action_4()
    {



    }

    private void Action_5()
    {


    }

    private void Action_6()
    {

    }
    public void Load_7()
    {

    }


    public void NextAction()
    {
        actionIndex++;

        actionList[actionIndex]();


    }


    private void MoveAction(GameObject obj,Vector2 Pos,int Index)
    {
        if(actionIndex==Index)
        {

            Vector2 dir = (Pos - (Vector2)obj.transform.position).normalized;

            if (Mathf.Abs(Pos.x - obj.transform.position.x) > 0.5f || Mathf.Abs(Pos.y - obj.transform.position.y) > 0.5f)
            {

                obj.transform.Translate(dir * 0.1f);
               
            }

           
        }


    }
}
