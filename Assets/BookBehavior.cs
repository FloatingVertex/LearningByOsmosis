using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehavior : MonoBehaviour
{
    public float ThrowSpeed;
    public float Gravity;
    public float InitialVerticalVelocity;
    public float ThrowHeight;
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
    private float _height;
    private float _verticalVelocity;

    // Use this for initialization
	void Start ()
    {
	}

    void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnter2D(other);
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
                    transform.localPosition = new Vector3(0, 1, 0);
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
                        Explode();
                    }
                }
                else if (other.gameObject.name == "WallBottom")
                {
                    
                }
                else if(other.GetComponent<BookBehavior>() == null)
                {
                    Explode();
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

    private void Explode()
    {
        Transform partT = transform.Find("particles");
        if (partT != null)
        {
            GameObject particles = partT.gameObject;
            particles.transform.parent = null;
            particles.SetActive(true);
            Destroy(gameObject);
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
            SetHeight(ThrowHeight);
            _verticalVelocity = InitialVerticalVelocity;
            _heldBy.GetComponent<RigidbodyController>().LoseBook();
            for (int i = 1; i < directions.Length; i++)
            {
                BookBehavior extraThrownBookBehavior = Instantiate(gameObject).GetComponent<BookBehavior>();
                extraThrownBookBehavior._throwDirection = directions[i].normalized;
                extraThrownBookBehavior.State = BookState.Thrown;
                extraThrownBookBehavior.GetComponent<CircleCollider2D>().enabled = true;
                extraThrownBookBehavior.SetHeight(ThrowHeight);
                extraThrownBookBehavior._heldBy = _heldBy;
                extraThrownBookBehavior._verticalVelocity = InitialVerticalVelocity;
                extraThrownBookBehavior.Kind = Kind;
            }
        }
        if (keep)
        {
            _heldBy.GetComponent<RigidbodyController>().GetBook(newBookBehavior);
        }
    }

    private void SetHeight(float height)
    {
        transform.Find("shadow").gameObject.SetActive(height > 0);
        transform.Find("shadow").gameObject.transform.localPosition = new Vector3(-0.02f, -0.4f - 0.4f * height, 1.83f);
        GetComponent<CircleCollider2D>().offset = new Vector2(0, -0.3f - 0.4f * height);
        _height = height;
    }

    // Update is called once per frame
    void Update ()
    {
        if (State == BookState.Thrown)
        {
            transform.position += _throwDirection * ThrowSpeed * Time.deltaTime;
            SetHeight(_height + _verticalVelocity * Time.deltaTime);
            _verticalVelocity -= Gravity * Time.deltaTime;
            if (_height <= 0)
            {
                Explode();
            }
        }
    }
}
