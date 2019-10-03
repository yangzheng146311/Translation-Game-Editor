using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FR_S6 : MonoBehaviour
{
    public GameObject inputframe;
    public GameObject subtitle;
    public GameObject correctResult;
    public GameObject wrongResult;
    public GameObject Theseus;
    public GameObject Periphetes;
    public AudioSource audioSource;

    public AudioClip[] music;

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
        actionList.Add(new Action(Action_7));
        actionList.Add(new Action(Action_8));
        actionList.Add(new Action(Action_9));
        actionList.Add(new Action(Action_10));
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
        if(Periphetes.GetComponent<Enemy_S6>().fight==false)
        MoveAction(Periphetes, new Vector2(-1.62f,-1.8f), 0);
       // MoveAction(Theseus, new Vector2(-3.26f, -1.99f), 1);


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





                    if (actionIndex != 10)
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
        ShowDialog(sourceText[0]);
        audioSource.clip = music[1];
        audioSource.Play();

    }

    private void Action_1()
    {
        dialogIndex++;
        ShowDialog(sourceText[1]);

    }

    private void Action_2()
    {
        dialogIndex++;
        ShowDialog(sourceText[2]);
        audioSource.clip = music[0];
        audioSource.Play();

    }

    private void Action_3()
    {
        dialogIndex++;
        ShowDialog(sourceText[3]);
        audioSource.clip = music[1];
        audioSource.Play();
    }

    private void Action_4()
    {
        dialogIndex++;
        ShowDialog(sourceText[4]);
        audioSource.clip = music[0];
        audioSource.Play();

    }

    private void Action_5()
    {
        dialogIndex++;
        ShowDialog(sourceText[5]);
        audioSource.clip = music[1];
        audioSource.Play();

    }

    private void Action_6()
    {
        dialogIndex++;
        ShowDialog(sourceText[6]);
    }

    private void Action_7()
    {
        dialogIndex++;
        ShowDialog(sourceText[7]);
    }

    private void Action_8()
    {
        dialogIndex++;
        ShowDialog(sourceText[8]);
    }

    private void Action_9()
    {
        audioSource.clip = music[2];
        audioSource.loop = true;
        audioSource.Play();
        Debug.Log("Fight");
        subtitle.SetActive(false);
        Theseus.GetComponent<Character_S6>().canMove = true;


        Periphetes.GetComponent<Enemy_S6>().fight = true;

    }

    private void Action_10()
    {
        SceneManager.LoadScene("FR_END");

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
