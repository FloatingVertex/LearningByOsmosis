using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using DG.Tweening;
using InControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehavior : MonoBehaviour
{
    public Button[] Buttons;
    public Text TimerText;

    private Player[] players;
    private float _countdownTime;

	// Use this for initialization
	void Start () {
		players = new Player[4];
	    _countdownTime = -100;
	    DOTween.Init();
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
	                    Buttons[i].transform.DOMoveY(Buttons[i].transform.position.y + 10, 1).SetEase(Ease.OutElastic);
	                    if (_countdownTime < -90 && players.Count(p=>p != null && p.Device.IsAttached) > 1)
	                    {
	                        _countdownTime = 10;
	                    }
	                    break;
	                }
	                if (players[i].Device == InputManager.Devices[i])
	                {
	                    break;
	                }

	            }
	        }
	    }
	    for (int i = 0; i < players.Length; i++)
	    {
	        if (players[i] != null)
	        {
	            Buttons[i].interactable = players[i].Device.IsAttached;
	        }
	    }
	    if (_countdownTime > 0)
	    {
	        _countdownTime -= Time.deltaTime;
            TimerText.text = ((int)_countdownTime + 1).ToString();
	    }
	    else if(_countdownTime > -90)
	    {
	        TimerText.text = "Starting...";
            SceneManager.LoadScene("EmptyMap");
	    }
	}
}
