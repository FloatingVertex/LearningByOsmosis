using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class ChangeBookAbility : MonoBehaviour {

    protected Queue<Vector3> oldPositions = new Queue<Vector3>();
    protected RigidbodyController controller;
    protected Player player;
    protected GameObject ghostObject;

    protected void Start()
    {
        controller = GetComponent<RigidbodyController>();
        player = controller.player;
    }
    // Update is called once per frame
    void Update()
    {
		if (player.Device.RightBumper.WasPressed)
        {
            controller.currentBook.Kind = (BookBehavior.KnowledgeType)(((int)controller.currentBook.Kind + 1)% 6);//6 = number of book types
        }
    }
}
