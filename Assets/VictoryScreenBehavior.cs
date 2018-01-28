using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class VictoryScreenBehavior : MonoBehaviour
{
    private float _countdownTimer;
	public AudioSource audioSource;
	//protected Player player;

    public Text Text;

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
		//audioSource = GetComponent<AudioSource>();

		//PlayerHolderBehavior.singleton.Players

	}
	
	// Update is called once per frame
	void Update ()
	{
        //TODO: Wait until the quote is done
	    _countdownTimer -= Time.deltaTime;
		if (!played && _countdownTimer < 20) {
			int track = Random.Range (0, 6);
		    SetText(quoteCategory, track);
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

    private void SetText(BookBehavior.KnowledgeType knowledgeType, int track)
    {
        Text.text = GetText(knowledgeType, track);
    }

    private string GetText(BookBehavior.KnowledgeType knowledgeType, int track)
    {
        switch (knowledgeType)
        {
            case BookBehavior.KnowledgeType.Art:
                switch (track)
                {
                    case 0:
                        return "The artist belongs to his work, not the work to the artist.  -Novalis";
                    case 1:
                        return "Creativity is allowing yourself to make mistakes.  Art is knowing which ones to keep.  -Scott Adams";
                    case 2:
                        return "You use a glass mirror to see your face; you use works of art to see your soul.  -George Bernard Shaw";
                    case 3:
                        return "The purpose of art is washing the dust of daily life off our souls.  -Pablo Picasso";
                    case 4:
                        return "Love of beauty is taste.  The creation of beauty is art.  -Ralph Waldo Emerson";
                }
                break;
            case BookBehavior.KnowledgeType.Language:
                switch (track)
                {
                    case 0:
                        return "Language is wine upon the lips.  -Virginia Woolf";
                    case 1:
                        return "The limits of my language means the limits of my world.  -Ludwig Wittgenstein";
                    case 2:
                        return "Every language is a world.  Without translation, we would inhabit parishes bordering on silence.  -George Steiner";
                    case 3:
                        return "A different language is a different vision of life.  -Federico Fellini";
                    case 4:
                        return "Language exerts hidden power, like the moon on the tides.  -Rita Mae Brown";
                }
                break;
            case BookBehavior.KnowledgeType.History:
                switch (track)
                {
                    case 0:
                        return "We are not makers of history.  We are made by history.  -Martin Luther King, Jr.";
                    case 1:
                        return "History never really says goodbye.  History says, ‘See you later.’ -Eduardo Galeano";
                    case 2:
                        return "There is nothing new in the world except the history you do not know.  -Harry S Truman";
                    case 3:
                        return "History is a cyclic poem written by time upon the memories of man.  -Percy Bysshe Shelley";
                    case 4:
                        return "History is not a burden on the memory but an illumination of the soul.  -Lord Acton";
                }
                break;
            case BookBehavior.KnowledgeType.Literature:
                switch (track)
                {
                    case 0:
                        return "Great literature is simply language charged with meaning to the utmost possible degree.  -Ezra Pound";
                    case 1:
                        return "Literature… is the union of suffering with the instinct for form.  -Thomas Mann";
                    case 2:
                        return "It’s in literature that true life can be found.  It’s under the mask of fiction that you can tell the truth.  -Gao Xingjian";
                    case 3:
                        return "Literature is the art of discovering something extraordinary about ordinary people, and saying with ordinary words something extraordinary.  -Boris Pasternak";
                    case 4:
                        return "Literature becomes the living memory of a nation.  -Aleksandr Solzhenitsyn";
                }
                break;
            case BookBehavior.KnowledgeType.Physics:
                switch (track)
                {
                    case 0:
                        return "Physics is the only profession in which prophecy is not only accurate but routine.  -Neil deGrasse Tyson";
                    case 1:
                        return "Physics has a history of synthesizing many phenomena into a few theories.  -Richard P. Feynman";
                    case 2:
                        return "Physics is experience, arranged in economical order.  -Ernst Mach";
                    case 3:
                        return "In science there is only physics; all the rest is stamp collecting.  -Lord Kelvin";
                    case 4:
                        return "Physics is about questioning, studying, probing nature.  You probe, and, if you’re lucky, you get strange clues.  -Lene Hau";
                }
                break;
            case BookBehavior.KnowledgeType.Math:
                switch (track)
                {
                    case 0:
                        return "Mathematics is the music of reason.  -James Joseph Sylvester";
                    case 1:
                        return "Mathematics is the most beautiful and most powerful creation of the human spirit.  -Stefan Banach";
                    case 2:
                        return "Mathematics is the art of giving the same name to different things.  -Henri Poincare";
                    case 3:
                        return "The essence of mathematics lies in its freedom.  -Georg Cantor";
                    case 4:
                        return "Pure mathematics is, in its way, the poetry of logical ideas.  -Albert Einstein";
                }
                break;
            default:
                throw new ArgumentOutOfRangeException("knowledgeType", knowledgeType, null);
        }
        return "Game Over";
    }
}
