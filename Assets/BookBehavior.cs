using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehavior : MonoBehaviour
{
    public float ThrowSpeed;

    public Sprite[] BookSprites;

    public Texture[] ParticleTextures;
    public Color[] ParticleColors;

    public enum KnowledgeType
    {
        Art,
        Language,
        History,
        Literature,
        Physics,
        Math
    }

    public KnowledgeType Kind
    {
        get { return _kind; }
        set
        {
            _kind = value;
            GetComponent<SpriteRenderer>().sprite = BookSprites[(int) value];
            transform.Find("particles").GetComponent<Renderer>().material.mainTexture = ParticleTextures[(int) value];
            ParticleSystem.MainModule main = transform.Find("particles").GetComponent<ParticleSystem>().main;
            main.startColor = ParticleColors[(int) value];
        }
    }

    public enum BookState { Grounded, Held, Thrown}

    public BookState State { get; private set; }

    private GameObject _heldBy;

    private Vector3 _throwDirection;
    private KnowledgeType _kind;

    // Use this for initialization
	void Start ()
    {
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
                    GetComponent<CircleCollider2D>().enabled = false;
                    character.GetBook(this);
                }
                break;
            case BookState.Held:
                break;
            case BookState.Thrown:
                if (character != null)
                {
                    if (character.gameObject != _heldBy)
                    {
                        character.HitByBook(this);
                        GameObject particles = transform.Find("particles").gameObject;
                        particles.transform.parent = null;
                        particles.SetActive(true);
                        Destroy(gameObject);
                    }
                }
                else if(other.GetComponent<BookBehavior>() == null)
                {
                    GameObject particles = transform.Find("particles").gameObject;
                    particles.transform.parent = null;
                    particles.SetActive(true);
                    Destroy(gameObject);
                }
                if (transform.position.magnitude > 20)
                {
                    Destroy(gameObject);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Throw(Vector3 fromPosition, bool keep, params Vector3[] directions)
    {
        BookBehavior newBookBehavior = null;
        if (keep)
        {
            newBookBehavior = Instantiate(gameObject, transform.parent).GetComponent<BookBehavior>();
            newBookBehavior.State = BookState.Held;
            newBookBehavior._heldBy = _heldBy;
            newBookBehavior.Kind = Kind;
        }
        if (directions[0].sqrMagnitude > 0)
        {
            transform.parent = null;
            _throwDirection = directions[0].normalized;
            State = BookState.Thrown;
            GetComponent<CircleCollider2D>().enabled = true;
            transform.Find("shadow").gameObject.SetActive(true);
            _heldBy.GetComponent<RigidbodyController>().LoseBook();
            for (int i = 1; i < directions.Length; i++)
            {
                BookBehavior extraThrownBookBehavior = Instantiate(gameObject).GetComponent<BookBehavior>();
                extraThrownBookBehavior._throwDirection = directions[i].normalized;
                extraThrownBookBehavior.State = BookState.Thrown;
                extraThrownBookBehavior.GetComponent<CircleCollider2D>().enabled = true;
                extraThrownBookBehavior.transform.Find("shadow").gameObject.SetActive(true);
                extraThrownBookBehavior._heldBy = _heldBy;
                extraThrownBookBehavior.Kind = Kind;
            }
        }
        if (keep)
        {
            _heldBy.GetComponent<RigidbodyController>().GetBook(newBookBehavior);
        }
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
