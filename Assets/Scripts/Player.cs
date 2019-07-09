using UnityEngine;
using System.Collections;
using UnityEngine.UI;	
using UnityEngine.SceneManagement;



	public class Player : MovingObject
	{
        public float turnDelay = 0.1f;
        public float moveSpeed = 0.1f;
        public static int level;
		public int pointsPerFood = 10;				
		public int pointsPerSoda = 20;				
        public static int pointsPerCoin = 2;
        public static float Damage = 0;	
        public static float HP=100;   
  
        /*		
        // Audio Clips
        // -----------      
        public AudioClip moveSound1;				
		public AudioClip moveSound2;				
		public AudioClip eatSound1;				
		public AudioClip eatSound2;					
		public AudioClip drinkSound1;				
		public AudioClip drinkSound2;				
		public AudioClip gameOverSound;             
        public AudioClip attackSound1;                      
        public AudioClip attackSound2;
        */       
     
        private Animator animator;                 //Used to store a reference to the Player's animator component.
        private Rigidbody2D r;

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        private Vector2 touchOrigin = -Vector2.one;	//Used to store location of screen touch origin for mobile controls.
#endif
    protected override void Start ()
		{    
            initPlayer();
			base.Start ();
		}


       private void initPlayer(){
        animator = GetComponent<Animator>();
       // GameManager.instance.restoreHP();
    }

    private void OnDisable()
    {
    //    GameManager.instance.restoreHP();

    }

		private void Update ()
		{

            int x = 0;  	
			int y = 0;		
			
#if UNITY_STANDALONE || UNITY_WEBPLAYER
						
			x = (int) (Input.GetAxisRaw ("Horizontal"));
			y = (int) (Input.GetAxisRaw ("Vertical"));

#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
			
			
			if (Input.touchCount > 0)
			{
			 Touch myTouch = Input.touches[0];
			
				if (myTouch.phase == TouchPhase.Began)
				{
					touchOrigin = myTouch.position;
				}
					else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
				{

                    	Vector2 touchEnd = myTouch.position;
						float a = touchEnd.x - touchOrigin.x;
						 float b = touchEnd.y - touchOrigin.y;
						touchOrigin.x = -1;
					
					if (Mathf.Abs(a) > Mathf.Abs(b))
						x = a > 0 ? 1 : -1;
					else
						y = b > 0 ? 1 : -1;
				}
			}

#endif

        if (x != 0)
        y = 0;
        flip(x);


        if (x != 0 || y != 0)
			{
            // RaycastHit2D hit;
            // bool canMove = Move(x, y, out hit);
            transform.position += new Vector3(x, y, 0)*moveSpeed;
        }    


		}



    protected override void OnCantMove<T>(T component)
    {

    }


    private void OnTriggerEnter2D (Collider2D other)
		{
			if(other.tag == "Exit")
			{
               Invoke ("YouWin",1.0f);
			}
				
			else if(other.tag == "Food")
			{
				HP += pointsPerFood;			
			//	SoundManager.instance.RandomizeSfx (eatSound1, eatSound2);
				other.gameObject.SetActive (false);
			}

            else if(other.tag == "Soda")
			{
            HP += pointsPerSoda;
            other.gameObject.SetActive(false);
            }
            else if (other.tag == "coin")
            {
            // money += pointsPerCoin;
            other.gameObject.SetActive(false);
            }
		}


   
    public void Restart ()
		{
       // GameManager.instance.saveData();
     //   SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
       // SoundManager.instance.musicSource.Play();
     //   restore();
      //  enabled = false;
    }

    public void YouWin()
    {
       // GameManager.instance.clearData();
       // GameManager.instance.win();
       // restore();
    }


    public void NewGame()
    {
     // GameManager.instance.clearData();
      //  Restart();
    }
    public void Quit()
    {
       // GameManager.instance.saveData();
        restore();
        Application.Quit();
    }

    public void restore()
    {
    
      //  DungeonGenerator.dungeonInstance.currentPos = Vector2.zero;
      //  DungeonGenerator.dungeonInstance.Dir = Vector2.zero;
     //   HP = GameManager.instance.maxHP;
      //  money = 0;
      //  weaponEquipped = false;
      //  weapons.damage = 0;
      //  Damage = baseDamage;
    }

    public void LoseHp (int loss)
		{
		animator.SetTrigger ("playerHit");
			HP -= loss;
			CheckIfGameOver ();
		}
		
		
		private void CheckIfGameOver ()
		{
        if (HP <= 0) 
			{
			//	SoundManager.instance.PlaySingle (gameOverSound);
			//	SoundManager.instance.musicSource.Stop();
			//	GameManager.instance.GameOver();
			}
		}



    public void flip(int x)
    {
      
         if (x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().flipX = true;
            }
            /*  if(weaponEquipped){
                 transform.GetChild(2).GetComponent<SpriteRenderer>().flipX = !weaponFlipped;
                 transform.GetChild(2).transform.localPosition = new Vector3(-0.45f, -0.04f, 0);
             }
             */

        }
        else if (x > 0)
        {
           GetComponent<SpriteRenderer>().flipX = false;
            for(int i=0;i< transform.childCount; i++) {
                transform.GetChild(i).GetComponent<SpriteRenderer>().flipX = false;
            }


            /*    if (weaponEquipped)
               {
                   transform.GetChild(2).GetComponent<SpriteRenderer>().flipX = weaponFlipped;
                   transform.GetChild(2).transform.localPosition = new Vector3(0.45f, -0.04f, 0);
               }
               */
        }
    }



	}

