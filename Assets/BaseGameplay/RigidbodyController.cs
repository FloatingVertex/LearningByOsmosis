using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class RigidbodyController : MonoBehaviour {

    public static int numberAlive = 0;

	bool[] activeEffects;
	int lifes;

	public bool splitShotOff;


    public Player player;

    public float movementSpeed = 1f;

    public BookBehavior currentBook;

    // Use this for initialization
    void Start()
    {
        numberAlive++;
		lifes = 6;
		activeEffects = new bool[6];
		splitShotOff = true;
    }

    private void FixedUpdate()
    {
        Move(player.Device.LeftStickX, player.Device.LeftStickY, Time.fixedDeltaTime, movementSpeed);
		if (player.Device.RightTrigger.WasPressed && HasBook() && splitShotOff)
        {
            currentBook.Throw(transform.position, false, player.Device.LeftStick);
        }
    }

    /// <summary>
    /// try to move this character based on input
    /// </summary>
    /// <param name="xAxis">range (-1,1) where 1 is 100% of movement speed</param>
    /// <param name="yAxis">range (-1,1) where 1 is 100% of movement speed</param>
    public void Move(float xAxis, float yAxis, float deltaTime,float movementSpeed)
    {
        //prevent faster diaganal movement
        var movement = new Vector2(xAxis, yAxis);
        if (movement.magnitude > 1.0f)
        {
            movement = movement.normalized;
        }

        GetComponent<Rigidbody2D>().AddForce(movement * movementSpeed, ForceMode2D.Force);
    }
    // Update is called once per frame
    void Update () {
		
	}

    public bool HasBook()
    {
        return currentBook != null;
    }

    public void GetBook(BookBehavior bookBehavior)
    {
        currentBook = bookBehavior;
        // May want to move the book to the hand position, once we get art and know where that is
    }

	// Deals with player being hit by the book
    public void HitByBook(BookBehavior bookBehavior)
    {
		
		//get which book hit the player
		BookBehavior.KnowledgeType adding = bookBehavior.Kind;
		//check if player was already hit by book
		if (!activeEffects[(int)adding]) {
			AddEffect (adding);
		}
    }
	private void AddEffect(BookBehavior.KnowledgeType kt){
        //TO DO: change players properties
        switch (kt)
        {
            case BookBehavior.KnowledgeType.Art:
                GetComponent<InvisibilityAbility>().enabled = true;
                break;
            case BookBehavior.KnowledgeType.History:
                GetComponent<RecallAbility>().enabled = true;
                break;
            case BookBehavior.KnowledgeType.Language:
                GetComponent<ChangeBookAbility>().enabled = true;
                break;
            case BookBehavior.KnowledgeType.Physics:
                GetComponent<DashAbility>().enabled = true;
                break;
        }
        //adds it to the affected array
        player.SetEffect(kt);
		activeEffects [(int)kt] = true;
		lifes--;
		//checks if player gets "killed"
		if (lifes == 0) {
            //TO DO kills player
            Destroy(gameObject);
		}
	}

    private void OnDestroy()
    {
        numberAlive--;
    }
    public void LoseBook()
    {
        currentBook = null;
    }
}
