﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class RecallAbility : CooldownAbility {

    public float tracebackTime = 1.0f;
    public float areaCheckRadius = 1.0f;
    public LayerMask areaCheckLayerMask;
    public GameObject ghostPrefab;

    protected Queue<Vector3> oldPositions = new Queue<Vector3>();
    protected RigidbodyController controller;
    protected Player player;
    protected GameObject ghostObject;

    protected void Start()
    {
        controller = GetComponent<RigidbodyController>();
        player = controller.player;
        ghostObject = Instantiate(ghostPrefab, transform.position, transform.rotation);
        ghostObject.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        CooldownUpdate();
        //recall if button is pressed
        if (player.Device.Action3.WasPressed && TryToUseAbility())
        {
            var positions = oldPositions.ToArray();
            bool abilityUsed = false;
            for(int i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                Vector2 positionToCheck = new Vector2(position.x, position.y);
                var raycastHit = Physics2D.OverlapCircle(positionToCheck, areaCheckRadius, areaCheckLayerMask);
                if(raycastHit == null)
                {
                    transform.position = position;
                    oldPositions = new Queue<Vector3>();
                    abilityUsed = true;
                    ghostObject.SetActive(false);
                    break;
                }
            }
            if(!abilityUsed)
            {
                abilityAvalibleToUse = true;
            }
        }
        if (oldPositions.Count > (tracebackTime / Time.fixedDeltaTime) / 2 && abilityAvalibleToUse)
        {
            ghostObject.transform.position = oldPositions.Peek();
            ghostObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        oldPositions.Enqueue(transform.position);
        if(oldPositions.Count > (tracebackTime/Time.fixedDeltaTime))
        {
            oldPositions.Dequeue();
        }
	}

    private void OnDestroy()
    {
        if(ghostObject != null)
        {
            Destroy(ghostObject);
        }
    }
}


