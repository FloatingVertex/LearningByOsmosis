using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class RigidbodyController : MonoBehaviour {
    public Player player;

    public float movementSpeed = 1f;

    private BookBehavior currentBook;

    // Use this for initialization
    void Start()
    {

    }

    private void FixedUpdate()
    {
        Move(player.Device.LeftStickX, player.Device.LeftStickY, Time.fixedDeltaTime);
    }

    /// <summary>
    /// try to move this character based on input
    /// </summary>
    /// <param name="xAxis">range (-1,1) where 1 is 100% of movement speed</param>
    /// <param name="yAxis">range (-1,1) where 1 is 100% of movement speed</param>
    public void Move(float xAxis, float yAxis, float deltaTime)
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

    public void HitByBook(BookBehavior bookBehavior)
    {
        /*TODO:
         * if(hasn't been hit by this kind yet)
         * {
         *  get hit by this kind
         * }
         */
    }
}
