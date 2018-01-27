using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehavior : MonoBehaviour
{
    public float ThrowSpeed;

    public Sprite[] BookSprites;

    public Material ParticleMaterial;
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
            ParticleMaterial.mainTexture = ParticleTextures[(int) value];
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
	void Start () {
	    State = BookState.Grounded;
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
                else
                {
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

    public bool Throw(Vector3 fromPosition, Vector3 direction)
    {
        if (direction.sqrMagnitude > 0)
        {
            transform.parent = null;
            //transform.position = fromPosition;
            _throwDirection = direction.normalized;
            State = BookState.Thrown;
            transform.Find("shadow").gameObject.SetActive(true);
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
