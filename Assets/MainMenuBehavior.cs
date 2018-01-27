using System.Collections;
using System.Collections.Generic;
using Assets;
using InControl;
using UnityEngine;

public class MainMenuBehavior : MonoBehaviour
{
    private Player[] players;
    private float _countdownTime;

	// Use this for initialization
	void Start () {
		players = new Player[4];
	}
	
	// Update is called once per frame
	void Update () {
	    for (int i = 0; i < InputManager.Devices.Count; i++)
	    {
	        if (InputManager.Devices[i].IsAttached && InputManager.Devices[i].AnyButtonWasPressed)
	        {
	            for (int j = 0; j < players.Length; j++)
	            {
	                if (players[i] == null)
	                {
                        players[i] = new Player(InputManager.Devices[i]);
	                    break;
	                }
	                if (players[i].Device == InputManager.Devices[i])
	                {
	                    break;
	                }

	            }
	        }
	    }
	}
}
