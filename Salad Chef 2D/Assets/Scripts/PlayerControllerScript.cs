using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {

    //Declaring key controllers for player movement
    public KeyCode moveForward;
    public KeyCode moveBackward;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode pickItem;
    public KeyCode dropItem;

    //Declaring initial speed of the player
    public float playerSpeed = 1.0f;

    //declaring and initializing inital time for the player 
    public float timeLeft = 120f;

    //Initial score of the player
    public int score = 0;

    //List to store the vegetables that the player had picked
    public List<GameObject> playerBasket;

    //List of vegetables that the player had dropped on to the chop board
    public List<GameObject> playerChopPlate;

    private float defaultSpeed;
    public bool canMove = true;
	// Use this for initialization
	void Start () {
        defaultSpeed = playerSpeed;
        playerBasket = new List<GameObject>(2);
        playerChopPlate = new List<GameObject>(3);

	}
	
	// Update is called once per frame
	void Update () {
        Timer();
        MovePlayer();
    }

    //FixedUpdate is called every fixed frames.
    //This is commonly used for rigidbody movements.
    private void FixedUpdate()
    {
       
    }

    //Timer function to controle the gametime left and to pause the player once the time is up
    void Timer()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            timeLeft = 0;
            canMove = false;
        }
        
    }

    //Move the player based on the key inputs.
    void MovePlayer()
    {
     
        if (canMove)
        {

            //Move forward
            if (Input.GetKey(moveForward))
            {
      
                this.gameObject.GetComponent<Rigidbody2D>().velocity=new Vector2(0,playerSpeed);
            }

            //Move backwards
            else if (Input.GetKey(moveBackward))
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -playerSpeed);
            }

            //Move Left
            else if (Input.GetKey(moveLeft))
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-playerSpeed, 0);
            }

            //Move right
            else if (Input.GetKey(moveRight)){
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(playerSpeed, 0);
            }

            // Stay in the same position if any other key is pressed
            else
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PickDrop(collision.collider);
    }

    void PickDrop(Collider2D item)
    {
        if (Input.GetKey(pickItem))
        {
            print(item);
            PickItem(item);
        }
        else if (Input.GetKey(dropItem))
        {

        }
    }

    //To pick particular vegetable from the rack.
    //The pick action is implemented based on the collider interaction of the player and the vegetables.
    void PickItem(Collider2D item)
    {
        if(playerBasket.Count <= 2)
        {
            if(item.gameObject.tag == "VEGETABLES")
            {
                GameObject vegItemPrefab = Instantiate(item.gameObject, transform.position, Quaternion.identity);
                playerBasket.Add(vegItemPrefab);
                vegItemPrefab.transform.parent = transform;
                Vector3 vegPos = vegItemPrefab.transform.position;
                vegPos.x += playerBasket.Count;
                vegItemPrefab.transform.position = vegPos;

                
            }

        }

    }
}
