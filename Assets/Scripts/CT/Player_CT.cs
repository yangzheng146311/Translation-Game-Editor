using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_CT : MonoBehaviour
{

    public GameObject jadeUI;
    int jadeNum = 0;
    public bool isFind;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(jadeUI.activeSelf)
        {

            time -= Time.deltaTime;
            if (time <=0)
                jadeUI.SetActive(false);
        }
    }

    
    //private void OnTriggerEnter2D(Collider2D collision)
    //{

       
    //    if (collision.name.Equals("dualistic_pair"))
    //    {
    //        collision.GetComponent<BoxCollider2D>().enabled = false;

    //        for (int i = 0; i < collision.transform.childCount; i++)
    //        {
    //            collision.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = true;
    //        }

    //        time = 3.0f;
    //        jadeUI.SetActive(true);
    //        jadeNum++;
    //        jadeUI.GetComponent<Text>().text = "瑤神美玉x" + jadeNum.ToString();


    //    }
    //}

  
}
