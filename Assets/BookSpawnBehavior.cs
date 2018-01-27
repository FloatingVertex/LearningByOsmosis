using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
	        var bookBehavior = Instantiate(BookPrefab, new Vector3(Random.value * 16 - 8, Random.value * 9 - 4.5f, 0),
	            Quaternion.identity).GetComponent<BookBehavior>();
	        bookBehavior.Kind = (BookBehavior.KnowledgeType) Random.Range(0, 6);
            _books.Add(bookBehavior);
	    }
	}

    public void RegisterPlayer(RigidbodyController controller)
    {
        _players.Add(controller);
    }
}
