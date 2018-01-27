using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehavior : MonoBehaviour
{
    public float ThrowSpeed;

    public enum KnowledgeType
    {
        Art,
        Language,
        History,
        Literatuer,
        Physics,
        Math
    }

    public KnowledgeType Kind { get; set; }

    public enum BookState { Grounded, Held, Thrown}

    public BookState State { get; private set; }

    private GameObject _heldBy;

    private Vector3 _throwDirection;

	// Use this for initialization
	void Start () {
	    State = BookState.Grounded;
	    Kind = KnowledgeType.Art;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        RigidbodyController character = other.GetComponent<RigidbodyController>();
        switch (State)
        {
            case BookState.Grounded:
                if (character != null && !character.HasBook())
                {
                    State = BookState.Held;
                    _heldBy = other.gameObject;
                    transform.parent = other.transform;
                    character.GetBook(this);
                }
                break;
            case BookState.Held:
                break;
            case BookState.Thrown:
                if (character != null && character.gameObject != _heldBy)
                {
                    character.HitByBook(this);
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public bool Throw(Vector3 fromPosition, Vector3 direction)
    {
        if (direction.sqrMagnitude > 0)
        {
            transform.parent = null;
            //transform.position = fromPosition;
            _throwDirection = direction.normalized;
            State = BookState.Thrown;
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update ()
    {
        if (State == BookState.Thrown)
        {
            transform.position += _throwDirection * ThrowSpeed * Time.deltaTime;
        }
	}
}
