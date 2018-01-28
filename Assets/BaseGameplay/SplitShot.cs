using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

[RequireComponent(typeof(RigidbodyController))]
public class SplitShot : MonoBehaviour {
	public float splitAngle = 30;
	protected RigidbodyController controller;

	protected Player player;

	protected void Start()
	{
		controller = GetComponent<RigidbodyController>();
		player = controller.player;
	}
	
	// Update is called once per frame
	void Update () {
		if (!controller.splitShotOff && (player.Device.RightTrigger.WasPressed || player.Device.RightBumper.WasPressed) && controller.HasBook())
		{
			Debug.Log("Triple shot attempt: ");

			Vector3[] directions = new Vector3[3];
			directions [0] = controller.lastMoved;
			directions [1] = Quaternion.Euler(0,0,splitAngle) * controller.lastMoved;
			directions [2] = Quaternion.Euler(0,0,-splitAngle) * controller.lastMoved;
			Debug.Log (directions.Length);
			for (int i = 0; i < 3; i++) {
				Debug.Log (directions [i].x);
				Debug.Log (directions [i].y);
				Debug.Log (directions [i].z);
			}
			if (controller.hold) {
				controller.currentBook.Throw (controller.transform.position, true, directions);
			    controller.hold = false;			} else {
				controller.currentBook.Throw (controller.transform.position, false, directions);

			}
		}
	}
}
