using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class PlayerHolderBehavior : MonoBehaviour
{
    public static PlayerHolderBehavior singleton;
    public List<Player> Players;

	void Start () {
        singleton = this;
		DontDestroyOnLoad(gameObject);
	}
}
