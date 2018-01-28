using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class PlayerHolderBehavior : MonoBehaviour
{
    public static PlayerHolderBehavior singleton;
    public List<Player> Players;
    public BookBehavior.KnowledgeType LastHit { get; private set; }

	void Start () {
        if(singleton != null)
        {
            Destroy(singleton);
        }
        singleton = this;
		DontDestroyOnLoad(gameObject);
	}

    public void RegisterHit(BookBehavior.KnowledgeType kt)
    {
        LastHit = kt;
    }
}
