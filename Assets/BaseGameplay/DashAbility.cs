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

    public GameObject ParticlePrefab;
    public AudioSource dashSource;
    public AudioClip dashSound;

    protected void Start()
    {
        controller = GetComponent<RigidbodyController>();
        player = controller.player;
        dashSource = (AudioSource)gameObject.AddComponent<AudioSource>();
        dashSound = (AudioClip)Resources.Load("dash");
        base.Start();
    }

    private void Update()
    {
        CooldownUpdate();
        if (player.Device.Action1.WasPressed && TryToUseAbility())
        {
            Debug.Log("Dash used by "+ player.Color);
            controller.Move(player.Device.LeftStickX, player.Device.LeftStickY,1f, dashForce);
            StartCoroutine(ApplyParticles());
            dashSource.PlayOneShot(dashSound);
        }
    }

    private IEnumerator ApplyParticles()
    {
        GameObject particles = Instantiate(ParticlePrefab, transform);
        particles.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(0.2f);
        ParticleSystem.EmissionModule mod = particles.GetComponent<ParticleSystem>().emission;
        mod.enabled = false;
    }
}
