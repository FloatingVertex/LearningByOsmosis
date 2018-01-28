using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class InvisibilityAbility : CooldownAbility
{

    public float invisLength = 1.0f;

    public GameObject VanishParticlePrefab;
    public GameObject AppearParticlePrefab;
    public AudioSource InvisAudio;
    public AudioClip InvisClipStart;
    public AudioClip InvisClipEnd;

    protected RigidbodyController controller;
    protected Player player;

    protected bool isVisible = true;
    protected float invisibilityTimer = 0.0f;

    protected void Start()
    {
        controller = GetComponent<RigidbodyController>();
        player = controller.player;
        InvisAudio = (AudioSource)gameObject.AddComponent<AudioSource>();
        InvisClipStart = (AudioClip)Resources.Load("invisibleIn");
        InvisClipEnd = (AudioClip)Resources.Load("InvisibleOut");
    }

    // Update is called once per frame
    void Update () {
        CooldownUpdate();

        //turn visible if timer ran out
        if (!isVisible)
        {
            invisibilityTimer += Time.deltaTime;
            if(invisibilityTimer > invisLength)
            {
                invisibilityTimer = 0.0f;
                isVisible = true;
                InvisAudio.PlayOneShot(InvisClipEnd);
                var componentsToDisable = GetComponentsInChildren<Renderer>();
                foreach (Renderer c in componentsToDisable)
                {
                    c.enabled = true;
                }
            }
        }

        //turn invisible if button is pressed
        if (player.Device.Action2.WasPressed && TryToUseAbility())
        {
            Debug.Log("Invisibility used by " + player.Color);
            var componentsToDisable = GetComponentsInChildren<Renderer>();
            foreach (Renderer c in componentsToDisable)
            {
                c.enabled = false;
            }
            isVisible = false;

            ParticleSystem vparticles = Instantiate(VanishParticlePrefab, transform)
                .GetComponent<ParticleSystem>();
            vparticles.GetComponent<Renderer>().material.mainTexture =
                controller.playerSprites[(int) controller.player.PlayerColor].texture;
            InvisAudio.PlayOneShot(InvisClipStart);

            ParticleSystem aparticles = Instantiate(AppearParticlePrefab, transform)
                .GetComponent<ParticleSystem>();
            aparticles.GetComponent<Renderer>().material.mainTexture =
                controller.playerSprites[(int)controller.player.PlayerColor].texture;
        }
    }
}
