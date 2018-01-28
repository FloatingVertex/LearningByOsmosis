using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUpdater : MonoBehaviour {

    public static BookBehavior.KnowledgeType[] pieOrder = { BookBehavior.KnowledgeType.Language, BookBehavior.KnowledgeType.Art, BookBehavior.KnowledgeType.Literature,
                                                            BookBehavior.KnowledgeType.Physics,BookBehavior.KnowledgeType.Math,BookBehavior.KnowledgeType.History};

    public static int IndexInPieOrder(BookBehavior.KnowledgeType kt)
    {
        for(int i = 0; i < pieOrder.Length; i++)
        {
            if(kt == pieOrder[i])
            {
                return i;
            }
        }
        throw new System.Exception();
    }

    public GameObject[] objectsToDisable;
    protected RigidbodyController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<RigidbodyController>();
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < pieOrder.Length;i++)
        {
            objectsToDisable[IndexInPieOrder((BookBehavior.KnowledgeType)i)].SetActive(!controller.player.activeEffects[i]);
        }
	}
}
