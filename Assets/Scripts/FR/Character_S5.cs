using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character_S5 : MonoBehaviour
{

    public bool canMove = true;
    private Animation animation;
    public float curHP;
    public GameObject slider;
    public GameObject fill;

    public GameObject dog1;
    public GameObject dog2;

    // Start is called before the first frame update
    void Start()
    {
        animation = transform.Find("handle").GetComponent<Animation>();
        curHP = 100;
      
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        Move();

        if (curHP <= 0)
        {


            SceneManager.LoadScene("FR_S5");
        }


        if(dog1.activeSelf==false&& dog2.activeSelf == false)
        {
            Debug.Log("Scene_S6_load");
            SceneManager.LoadScene("FR_S6");
        }
       
    }



    private void Move()
    {
       
        gameObject.transform.Translate(Vector3.right* Input.GetAxis("Horizontal")*0.1f);


        if(Input.GetKeyUp(KeyCode.W))
        {


            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*5, ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {


            animation.Play();
        }



    }

   
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
      
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(curHP);
        if (collision.gameObject.name == "dog")
        {

            if (curHP > 0)
            {
                curHP -= 20;
                slider.GetComponent<Slider>().value = (curHP / 100);
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

}
