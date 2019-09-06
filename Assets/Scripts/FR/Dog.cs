using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dog : MonoBehaviour
{
    float curHP;
    public GameObject slider;
    public GameObject fill;

    // Start is called before the first frame update
    void Start()
    {
        curHP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(curHP<=0)
        {


            gameObject.SetActive(false);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "sword")
        {
          
            if (curHP >0)
            {
                curHP -= 20;
                slider.GetComponent<Slider>().value =(curHP / 100);
                Debug.Log(slider.GetComponent<Slider>().value);
            }
           
        }
        if (curHP >= 60)
            fill.GetComponent<Image>().color = Color.green;
        if (curHP >= 40 && curHP < 60)
            fill.GetComponent<Image>().color = Color.yellow;
        if (curHP >= 0 && curHP < 40)
            fill.GetComponent<Image>().color = Color.red;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name== "Theseus")
        {
            if (gameObject.GetComponent<Animation>().isPlaying == false)
            {
                gameObject.GetComponent<Animation>().Play();
                
            }
            

        }
    }

}
