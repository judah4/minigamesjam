using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class CharacterSetup
{
    public string Name;
    public GameObject Model;
    public AudioClip Footsteps;
}

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class AnimalController : MonoBehaviour
{

    public List<CharacterSetup> CharacterSetup;
    private int characterId = 0;

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

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioSource _soundEffectSource;
    [SerializeField]
    private AudioClip _bounceSound;

    [SerializeField]
    private List<AudioClip> _eatClips;


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

    public string CharacterName
    {
        get { return CharacterSetup[characterId].Name; }
    }

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


        var tween = CharacterSetup[characterId].Model.transform.DOLocalRotate(new Vector3(0, 0, 10), .5f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo);

    }
	
	// Update is called once per frame
	void Update ()
	{

	    if (Frozen)
	    {
	        if (_floatPos != null)
	        {
                _rigidbody.MovePosition(Vector3.Lerp(transform.position, _floatPos.Value, Time.deltaTime * 10));
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
	        if (_audioSource.clip != CharacterSetup[characterId].Footsteps)
	        {
	            _audioSource.clip = CharacterSetup[characterId].Footsteps;

	        }

	        if (_audioSource.isPlaying == false)
	        {
                _audioSource.Play();
	        }

	        var rot = Quaternion.LookRotation(_movement);
	        _rigidbody.transform.rotation = Quaternion.Lerp(_rigidbody.transform.rotation, rot, 10 * Time.deltaTime);
	    }
	    else
	    {
	        if (_audioSource.isPlaying)
	        {
	            _audioSource.Stop();
	        }

        }
    }

    public void Stun(float length)
    {
        _lockTime = Time.time + length;

        if (_soundEffectSource == null)
            return;

        _soundEffectSource.clip = _bounceSound;
        _soundEffectSource.Play();

    }

    public void SetCharacter(int charId)
    {
        characterId = charId;
        for (int cnt = 0; cnt < CharacterSetup.Count; cnt++)
        {
            CharacterSetup[cnt].Model.SetActive(characterId == cnt);
            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Character collide " + collision.transform.name);

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

    public void PlayEatEffect()
    {
        if(_soundEffectSource == null)
            return;

        _soundEffectSource.clip = _eatClips[Random.Range(0, _eatClips.Count)];
        _soundEffectSource.Play();
    }
}
