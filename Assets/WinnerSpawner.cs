using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class WinnerSpawner : MonoBehaviour {

    public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
        foreach(Player p in PlayerHolderBehavior.singleton.Players)
        {
            int effects = 0;
            foreach(bool b in p.activeEffects)
            {
                if (b) effects++;
            }
            if(effects < 6)
            {
                var playerGO = Instantiate(playerPrefab, transform.position, transform.rotation);
                playerGO.GetComponent<RigidbodyController>().player = p;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
