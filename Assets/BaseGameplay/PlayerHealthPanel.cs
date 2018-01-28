using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthPanel : MonoBehaviour {
    public int playerId = 0;

    public GameObject[] activatablePieces;

    public Image[] imagesToRecolor;

    private void Start()
    {
        if(PlayerHolderBehavior.singleton.Players.Count <= playerId)
        {
            gameObject.SetActive(false);
            return;
        }
        var player = PlayerHolderBehavior.singleton.Players[playerId];
        foreach(Image img in imagesToRecolor)
        {
            img.color = player.Color;
        }
    }

    // Update is called once per frame
    void Update () {
        var player = PlayerHolderBehavior.singleton.Players[playerId];
        for(int i = 0; i < player.activeEffects.Length; i++)
        {
            if (activatablePieces[i] == null || player == null || player.activeEffects == null)
            {
                Debug.Log("Null");
            }
            else
            {
                activatablePieces[i].SetActive(player.activeEffects[i]);
            }
        }
	}
}
