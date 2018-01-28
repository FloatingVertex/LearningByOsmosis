using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthPanel : MonoBehaviour {
    public int playerId = 0;

    public GameObject[] activatablePieces;

    private void Start()
    {
        if(PlayerHolderBehavior.singleton.Players.Count <= playerId)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
        var player = PlayerHolderBehavior.singleton.Players[playerId];
        for(int i = 0; i < player.activeEffects.Length; i++)
        {
            activatablePieces[i].SetActive(player.activeEffects[i]);
        }
	}
}
