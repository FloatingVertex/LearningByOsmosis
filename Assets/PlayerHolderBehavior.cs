using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class PlayerHolderBehavior : MonoBehaviour
{
    public List<Player> Players;

	void Start () {
		DontDestroyOnLoad(gameObject);
	}
}
