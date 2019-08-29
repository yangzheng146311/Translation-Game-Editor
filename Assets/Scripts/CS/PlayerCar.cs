using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    private float speed;
    private bool isDizzy;
    private bool isGod;
    
    private Vector3 direction;

    float dizzyTime;
    float speedTime;
    float squatTime;
    float godTime;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
      
        isGod = false;
        direction = new Vector2(-1, 0);

        dizzyTime = 0;

        speedTime = 0;

        godTime = 0;

      
        
    }

    // Update is called once per frame
    void Update()
    {

        if (dizzyTime > 0)
        {
            Dizzy();
            dizzyTime -= Time.deltaTime;
        }


       


        else
        {
            if(CS_GameManager.GetManager().bGameStop==false)
            Move();
        }
    }

    private void SpeedUp()
    {
        speed = 2.0f;
    }

    private void Move()
    {


        if (!isDizzy)
        {
            Camera.main.transform.SetPositionAndRotation(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10), Quaternion.identity);
           

            if (speedTime > 0)
            {

                SpeedUp();
                speedTime -= Time.deltaTime;

            }

            else
                speed = 1.0f;


            if (godTime > 0)
            {
                isGod = true;
                godTime -= Time.deltaTime;

                SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
                render.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                isGod = false;
                SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
                render.color = new Color(1, 1, 1, 1);
            }

        

            gameObject.transform.Rotate(0, 0, -2.0f * Input.GetAxis("Horizontal"));

            gameObject.transform.Translate(direction * Input.GetAxis("Vertical") * speed*0.5f);
        }


       

    }


    private void Dizzy()
    {
       
        gameObject.transform.Rotate(0, 0, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag.Equals("Coin"))
        {

            collision.gameObject.SetActive(false);
            CS_GameManager.GetManager().AddCoin(1);
        }


        if (collision.tag.Equals("CS_RoadNode"))
        {

          int roadIndex=int.Parse(collision.name.Split('_')[1])-1;

          int nextIndex = CS_GameManager.GetManager().GetNextIndex();


           

            if (roadIndex == nextIndex)
            {


                if (roadIndex == CS_GameManager.GetManager().lastNodeIndex)
                {

                    Debug.Log("endgame");
                    CS_GameManager.GetManager().EndGame();
                }
                else
                {
                    
                    CS_GameManager.GetManager().nextNodeIndex++;
                }
            }


        }


        if(collision.name.Equals("Cone")|| collision.name.Equals("Bell") || collision.name.Equals("Soil") || collision.name.Equals("Gan"))
        {

            collision.gameObject.SetActive(false);

            if(isGod!=true)
            dizzyTime = 2;
            
        }

        if (collision.name.Equals("Rocket"))
        {

            collision.gameObject.SetActive(false);
            speedTime = 2;
        }

        if (collision.name.Equals("Star"))
        {

            collision.gameObject.SetActive(false);
            godTime = 2;
        }

    }

   








}
