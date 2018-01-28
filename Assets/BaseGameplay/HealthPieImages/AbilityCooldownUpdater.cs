using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets;

[RequireComponent(typeof(Slider))]
public class AbilityCooldownUpdater : MonoBehaviour {
    public bool UpdateCooldown;
    public Image sliderFill;
    public string type;
    public int playerId;

    protected Player player;
    protected Slider slider;
    protected Color startingColor;

    private void Start()
    {
        if (PlayerHolderBehavior.singleton.Players.Count <= playerId)
        {
            gameObject.SetActive(false);
            return;
        }
        player = PlayerHolderBehavior.singleton.Players[playerId];
        slider = GetComponent<Slider>();
        startingColor = sliderFill.color;
    }

    private void Update()
    {
        if(player.controller == null)
        {
            gameObject.SetActive(false);
            return;
        }
        var script = player.controller.GetComponent(type) as MonoBehaviour;
        sliderFill.gameObject.SetActive(script.enabled);
        if(script is CooldownAbility)
        {
            if((script as CooldownAbility).abilityAvalibleToUse)
            {
                slider.value = 1.0f;
                sliderFill.color = startingColor;
            }
            else
            {
                slider.value = (script as CooldownAbility).abilityTimer / (script as CooldownAbility).cooldown;
                var fadedColor = startingColor;
                fadedColor.a = 0.6f;
                sliderFill.color = fadedColor;
            }
        }
    }
}
