using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

[RequireComponent(typeof(RigidbodyController))]
public class DashAbility : CooldownAbility
{
    public float dashForce = 1000f;

    protected RigidbodyController controller;
    protected Player player;
    protected void Start()
    {
        controller = GetComponent<RigidbodyController>();
        player = controller.player;
    }

    private void Update()
    {
        CooldownUpdate();
        if (player.Device.Action1.WasPressed && TryToUseAbility())
        {
            Debug.Log("Dash used by "+ player.Color);
            controller.Move(player.Device.LeftStickX, player.Device.LeftStickY,1f, dashForce);
        }
    }
}
