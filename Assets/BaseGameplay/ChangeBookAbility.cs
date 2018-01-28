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
		if (player.Device.Action4.WasPressed)
        {
            if (controller.currentBook != null)
            {
                int oldIdx = HealthBarUpdater.IndexInPieOrder(controller.currentBook.Kind);
                controller.currentBook.Kind = HealthBarUpdater.pieOrder[(oldIdx + 1) % 6];//6 = number of book types
            }
        }
    }
}
