using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedChanger : MonoBehaviour {

    public float gamespeed = 1.0f;

	// Use this for initialization
	void Start () {
        Time.timeScale = gamespeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
