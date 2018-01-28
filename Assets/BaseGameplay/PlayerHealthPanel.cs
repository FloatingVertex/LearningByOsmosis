using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthPanel : MonoBehaviour {
    public int playerId = 0;

    public GameObject[] activatablePieces;

    public Image[] imagesToRecolor;

    public GameObject[] disableOnDeath;
    public Image fadeOnDeath;

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
        if(player.controller == null)
        {
            foreach(GameObject go in disableOnDeath)
            {
                go.SetActive(false);
            }
            var originalColor = fadeOnDeath.color;
            originalColor.a = 0.5f;
            fadeOnDeath.color = originalColor;
            enabled = false;
            return;
        }
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
