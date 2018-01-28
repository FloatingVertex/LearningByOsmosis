using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject[] spawnPoints;

	// Use this for initialization
    void Awake()
    {
        BookSpawnBehavior bookSpawner = GetComponent<BookSpawnBehavior>();
        var playersList = PlayerHolderBehavior.singleton.Players;
        for (int i = 0; i < playersList.Count; i++)
        {
            var playerObject = Instantiate(playerPrefab, spawnPoints[i].transform.position,
                spawnPoints[i].transform.rotation);
            playerObject.GetComponent<RigidbodyController>().player = playersList[i];
            bookSpawner.RegisterPlayer(playerObject.GetComponent<RigidbodyController>());
        }
    }

    // Update is called once per frame
	void Update () {
		
	}
}
