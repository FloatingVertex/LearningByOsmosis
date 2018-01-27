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

    enum BookState { Grounded, Held, Thrown}

    private BookState _state;

    private GameObject _heldBy;

    private Vector3 _throwDirection;

	// Use this for initialization
	void Start () {
		_state = BookState.Grounded;
	    Kind = KnowledgeType.Art;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        RigidbodyController character = other.GetComponent<RigidbodyController>();
        switch (_state)
        {
            case BookState.Grounded:
                if (character != null && !character.HasBook())
                {
                    _state = BookState.Held;
                    _heldBy = other.gameObject;
                    transform.parent = other.transform;
                    character.GetBook(this);
                }
                break;
            case BookState.Held:
                break;
            case BookState.Thrown:
                Destroy(gameObject);
                if (character != null && character.gameObject != _heldBy)
                {
                    character.HitByBook(this);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Throw(Vector3 fromPosition, Vector3 direction)
    {
        transform.position = fromPosition;
        _throwDirection = direction;
        _state = BookState.Thrown;
    }

    // Update is called once per frame
    void Update ()
    {
        if (_state == BookState.Thrown)
        {
            transform.position += _throwDirection * ThrowSpeed * Time.deltaTime;
        }
	}
}
