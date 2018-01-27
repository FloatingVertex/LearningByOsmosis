using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BookSpawnBehavior : MonoBehaviour
{
    public GameObject BookPrefab;

    private List<RigidbodyController> _players = new List<RigidbodyController>();
    private List<BookBehavior> _books = new List<BookBehavior>();

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (_books.Count(b => b.State == BookBehavior.BookState.Grounded) < 2)
	    {
	        //Spawn a book!
	        Vector3 randomPosition = new Vector3(Random.value * 16 - 8, Random.value * 9 - 4.5f, 0);
	        Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPosition, 1f);
	        while (colliders.Length > 0)
	        {
	            randomPosition = new Vector3(Random.value * 16 - 8, Random.value * 9 - 4.5f, 0);
	            colliders = Physics2D.OverlapCircleAll(randomPosition, 1f);
	        }
            var bookBehavior = Instantiate(BookPrefab, randomPosition,
	            Quaternion.identity).GetComponent<BookBehavior>();

            List<BookBehavior.KnowledgeType> potentialKinds = new List<BookBehavior.KnowledgeType>
            {
                BookBehavior.KnowledgeType.Art,
                BookBehavior.KnowledgeType.Language,
                BookBehavior.KnowledgeType.History,
                BookBehavior.KnowledgeType.Literature,
                BookBehavior.KnowledgeType.Physics,
                BookBehavior.KnowledgeType.Math
            };

	        for (int i = _players.Count - 1; i >= 0; i--)
	        {
	            if (_players[i] == null)
	            {
	                _players.RemoveAt(i);
	            }
	            else
	            {
	                for (int j = 0; j < 6; j++)
	                {
	                    if (!_players[i].HasBeenHitBy((BookBehavior.KnowledgeType) j))
	                    {
	                        potentialKinds.Add((BookBehavior.KnowledgeType) j);
	                    }
	                }
	            }
	        }

	        bookBehavior.Kind = potentialKinds[Random.Range(0, potentialKinds.Count)];
            _books.Add(bookBehavior);
	    }
	}

    public void RegisterPlayer(RigidbodyController controller)
    {
        _players.Add(controller);
    }
}
