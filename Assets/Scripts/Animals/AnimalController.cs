using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class AnimalController : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider _collider;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private Vector3 _movement;

    [SerializeField]
    private float _moveSpeed = 5;

    [SerializeField]
    private int _life = 10;

    [SerializeField]
    private float _lockTime = 0;

    [SerializeField]
    private float _pushPower = 9;

    public bool Dead
    {
        get { return _life < 1; }
    }

    public Rigidbody Rigidbody
    {
        get { return _rigidbody; }
    }

    private bool _loadingIn = false;
    public bool Frozen
    {
        get { return  _rigidbody.isKinematic; }
    }

    public int Life
    {
        get { return _life; }
    }

    public string CharacterName;

    private Vector3? _floatPos;


    public FruitEvent OnLifeChange;

    // Use this for initialization
    void Start()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        if (_collider == null)
        {
            _collider = GetComponent<CapsuleCollider>();
        }
    }
	
	// Update is called once per frame
	void Update ()
	{

	    if (Frozen)
	    {
	        if (_floatPos != null)
	        {
                _rigidbody.MovePosition(Vector3.Lerp(transform.position, _floatPos.Value, Time.deltaTime * 5));
	        }
	    }

	    if (_floatPos == null)
	    {
	        _loadingIn = false;
	        _rigidbody.isKinematic = false;
        }

        if (_loadingIn)
	    {
	        
	        if (Vector3.Distance(_rigidbody.position, _floatPos.Value) < .5f)
	        {
	            _loadingIn = false;
	            _rigidbody.isKinematic = false;
            }
	    }

	    if (Dead)
	    {
            gameObject.SetActive(false);
	    }

        if(Dead || Frozen)
            return;

	    if (Time.time > _lockTime)
	    {

	        _rigidbody.velocity = _movement * _moveSpeed;
	    }

	    if (_movement.magnitude > 0)
	    {
	        var rot = Quaternion.LookRotation(_movement);
	        _rigidbody.transform.rotation = Quaternion.Lerp(_rigidbody.transform.rotation, rot, 10 * Time.deltaTime);
	    }
	}

    public void Stun(float length)
    {
        _lockTime = Time.time + length;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Character collide " + collision.transform.name);

        Rigidbody body = collision.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        var animal = collision.gameObject.GetComponent<AnimalController>();
        if (animal == null)
        {
            return;

        }

        animal.Stun(1);

        var dir = collision.transform.position - transform.position;

        animal.Rigidbody.AddForce(dir * _pushPower, ForceMode.Impulse);

        //Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        //body.velocity = pushDir * 10;
    }

    public void Move(Vector3 movement)
    {
        _movement = movement;
        _movement.y = 0;
    }

    public void Freeze(bool freeze, Vector3? pos = null)
    {
        if (freeze)
        {
            _loadingIn = false;
            _rigidbody.isKinematic = true;
        }
        else
        {
            _loadingIn = true;
        }

        _floatPos = pos;
    }

    public void AddLife(int amt)
    {
        _life += amt;

        OnLifeChange.Invoke(_life);
    }
}
