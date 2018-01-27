using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenBehavior : MonoBehaviour
{
    private float _countdownTimer;
	// Use this for initialization
	void Start ()
	{
	    _countdownTimer = 5;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _countdownTimer -= Time.deltaTime;
	    if (_countdownTimer < 0)
	    {
	        SceneManager.LoadScene("MainMenu");
	    }
	}
}
