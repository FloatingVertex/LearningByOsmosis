using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for cooldown abilities
/// </summary>
public class CooldownAbility : MonoBehaviour {

    public float cooldown = 10f;
    [HideInInspector]
    public float abilityTimer = 0.0f;
    [HideInInspector]
    public bool abilityAvalibleToUse = true;
    public AudioSource coolSource;
    public AudioClip coolClip;

    protected void Start()
    {
        coolSource = (AudioSource)gameObject.AddComponent<AudioSource>();
        coolClip = (AudioClip)Resources.Load("cooldownover");
    }

    protected bool TryToUseAbility()
    {
        if (!abilityAvalibleToUse)
        {
            return false;
        }
        abilityAvalibleToUse = false;
        return true;
    }

    protected void CooldownUpdate()
    {
        if(!abilityAvalibleToUse)
        {
            abilityTimer += Time.deltaTime;
            if(abilityTimer > cooldown)
            {
                abilityTimer = 0f;
                abilityAvalibleToUse = true;
                coolSource.PlayOneShot(coolClip);
            }
        }
    }
}
