using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_S4 : MonoBehaviour
{

    public bool canMove = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        Move();
    }



    private void Move()
    {
       
        gameObject.transform.Translate(Vector3.right* Input.GetAxis("Horizontal")*0.1f);


        if(Input.GetKeyUp(KeyCode.W))
        {


            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*5, ForceMode2D.Impulse);
        }


    }


   

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name.Equals("sword"))
        {


            if (Input.GetKeyUp(KeyCode.Space))
            {

                collision.gameObject.SetActive(false);

                transform.Find("sword").gameObject.SetActive(true);


                FR_S4 fr_s4 = GameObject.Find("GameManager").GetComponent<FR_S4>();
                if (fr_s4)
                {

                   fr_s4.NextAction();

                }
            }
        }
    }

}
