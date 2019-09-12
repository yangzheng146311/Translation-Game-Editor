using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy_S7 : MonoBehaviour
{

   
   
    public float curHP;
    public GameObject slider;
    public GameObject fill;

    float attacktime = 0;

    
   

    // Start is called before the first frame update
    void Start()
    {
        //animation = transform.Find("handle").GetComponent<Animation>();
        curHP = 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        attacktime += Time.deltaTime;

        if(attacktime>1.0f)
        {


            attacktime = 0;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5.0f, ForceMode2D.Impulse);
        }





    }



   
   
    private void OnCollisionEnter2D(Collision2D collision)
    {

     
        Debug.Log(curHP);
        if (collision.gameObject.name!= "Floor")
        {

            if (curHP > 0)
            {
                curHP -= 20;
                slider.GetComponent<Slider>().value = (curHP / 100);
                Debug.Log(slider.GetComponent<Slider>().value);

                if (curHP == 0)
                {


                    Debug.Log("Win");
                    
                    gameObject.SetActive(false);

                    //FR_S6 fr_s6 = GameObject.Find("GameManager").GetComponent<FR_S6>();
                    //if (fr_s6)
                    //{

                    //    fr_s6.NextAction();

                    //}
                }
            }


        }
        if (curHP >= 60)
            fill.GetComponent<Image>().color = Color.green;
        if (curHP >= 40 && curHP < 60)
            fill.GetComponent<Image>().color = Color.yellow;
        if (curHP >= 0 && curHP < 40)
            fill.GetComponent<Image>().color = Color.red;
    }

    
}
