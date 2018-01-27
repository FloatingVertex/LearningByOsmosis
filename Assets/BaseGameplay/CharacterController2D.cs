using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    // controlling player
    [Range(0, 3)] public int playerIndex = 0;

    public float movementSpeed = 1f;
    public LayerMask movementMask;
    public Transform feet;
    public float feetRadius;

    // match to how the input manager is setup
    [SerializeField]
    protected string[] walkingXAxisList;
    [SerializeField]
    protected string[] walkingYAxisList;
    [SerializeField]
    protected string[] ability1Button;

	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        Move(Input.GetAxis(walkingXAxisList[playerIndex]), Input.GetAxis(walkingYAxisList[playerIndex]),Time.fixedDeltaTime);
    }

    /// <summary>
    /// try to move this character based on input
    /// </summary>
    /// <param name="xAxis">range (-1,1) where 1 is 100% of movement speed</param>
    /// <param name="yAxis">range (-1,1) where 1 is 100% of movement speed</param>
    public void Move(float xAxis,float yAxis,float deltaTime)
    {
        //prevent faster diaganal movement
        var movement = new Vector2(xAxis, yAxis);
        if(movement.magnitude > 1.0f)
        {
            movement = movement.normalized;
        }

        var contactMask = new ContactFilter2D();
        RaycastHit2D[] raycastHits = new RaycastHit2D[100];
        contactMask.layerMask = movementMask;
        int numberOfHits = Physics2D.CircleCast(feet.position, feetRadius, movement, contactMask,raycastHits, movementSpeed * movement.magnitude * deltaTime);

        Vector2 initialPosition = new Vector2(feet.position.x, feet.position.y);
        bool hitSomething = false;
        for(int i = 0; i < numberOfHits; i++)
        {
            if(raycastHits[i].centroid != initialPosition)
            {
                Debug.Log("Blocked");
                var newPosition = (Vector3)raycastHits[i].centroid + (transform.position - feet.position);
                newPosition.z = transform.position.z;
                newPosition += (newPosition - transform.position).normalized * -0.1f;
                transform.position = newPosition;
                hitSomething = true;
                break;
            }
        }
        if (!hitSomething)
        {
            transform.position = transform.position + ((Vector3)movement * (movementSpeed * deltaTime));
        }

        //RaycastHit2D raycastHit = Physics2D.CircleCast(feet.position, feetRadius, movement, movementSpeed * movement.magnitude * deltaTime, movementMask);

        //if raycasty hit something
        //if (raycastHit.collider)
        //{
        //    transform.position = (Vector3)raycastHit.centroid + (transform.position - feet.position);
        //}
        //else
        //{
        //    transform.position = transform.position + ((Vector3)movement * (movementSpeed * deltaTime));
        //}
    }

    // Update is called once per frame
    void Update () {
		
	}
}
