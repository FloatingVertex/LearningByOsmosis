using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets;

public class VictoryScreenBehavior : MonoBehaviour
{
    private float _countdownTimer;
	public AudioSource audioSource;
	//protected Player player;

	AudioClip toPlay;
	protected bool played;
	BookBehavior.KnowledgeType quoteCategory;
	public AudioClip[] clips;
	// Use this for initialization
	void Start ()
	{
	    _countdownTimer = 22;
	    quoteCategory = PlayerHolderBehavior.singleton.LastHit;
		played = false;
        //TODO: Load up a quote
		audioSource = GetComponent<AudioSource>();

		//PlayerHolderBehavior.singleton.Players

	}
	
	// Update is called once per frame
	void Update ()
	{
        //TODO: Wait until the quote is done
	    _countdownTimer -= Time.deltaTime;
		if (!played && _countdownTimer < 20) {
			int track = Random.Range (0, 6);
			audioSource.PlayOneShot (clips [(int)quoteCategory * 5 + track]);
			played = true;
		}
		if (_countdownTimer < 9) {
			for (int i = 0; i < PlayerHolderBehavior.singleton.Players.Count; i++) {
				if (PlayerHolderBehavior.singleton.Players [i].Device.Action1) {
					_countdownTimer -= 2;
				}
			}
		}
	    if (_countdownTimer < 0)
	    {
	        SceneManager.LoadScene("MainMenu");
	    }
	}
}
