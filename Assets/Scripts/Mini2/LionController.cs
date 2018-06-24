using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class LionController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _runningClip;

    private Transform _target;
    [SerializeField]
    private Transform _lionModel;

    [SerializeField] private NavMeshAgent _agent;
    private float _startAgentSpeed = 10;
    [SerializeField]
    private float _startAgentAngular = 10;
    private float _startAgentAccel = 10;
    [SerializeField]
    private float _pushPower = 9;

    private float _lastAttackTime = 0;
    private Transform _lastTargeted;

    private Sequence moveTween;
    private Sequence holdTween;

    private float _waitTimer = -1;

    // Use this for initialization
    void Start ()
    {
        _startAgentSpeed = _agent.speed;
        _startAgentAngular = _agent.angularSpeed;
        _startAgentAccel = _agent.acceleration;

        holdTween = DOTween.Sequence();
        holdTween.Append(_lionModel.DOLocalRotate(new Vector3(0, 0, 0), .5f)).SetLoops(-1);
        holdTween.Pause();

        moveTween = DOTween.Sequence();
        moveTween.Append(_lionModel
            .DOLocalRotate(new Vector3(0, 0, 10), .2f, RotateMode.Fast));
        moveTween.Append(_lionModel
            .DOLocalRotate(new Vector3(0, 0, -10), .2f, RotateMode.Fast));
        moveTween.SetLoops(-1, LoopType.Yoyo);
        moveTween.Pause();

    }
	
	// Update is called once per frame
	void Update ()
	{

	    if (_waitTimer != -1)
	    {
	        if (Time.time > _waitTimer)
	        {
                Wait(false);
	            _waitTimer = -1;
	        }
	    }

	    if (!_agent.isStopped)
	    {
	        if (moveTween.IsPlaying() == false)
	        {
	            moveTween.Play();
	            holdTween.Pause();
	        }

	        if (_audioSource.isPlaying == false)
	        {
	            _audioSource.Play();
	        }

	    }
	    else
	    {
	        if (holdTween.IsPlaying() == false)
	        {
	            holdTween.Play();
	            moveTween.Pause();
	        }

	        if (_audioSource.isPlaying)
	        {
	            _audioSource.Stop();
	        }

        }

	    _agent.speed = _startAgentSpeed + GamaManager.Instance.Level;
	    _agent.angularSpeed = _startAgentAngular + GamaManager.Instance.Level * 5;
	    _agent.acceleration = _startAgentAccel + GamaManager.Instance.Level;

        AnimalController closest = null;
	    for (int cnt = 0; cnt < GamaManager.Instance.Players.Count; cnt++)
	    {
	        if (GamaManager.Instance.Players[cnt].Dead)
	        {

	            continue;
	        }

	        if (closest == null)
	        {
	            closest = GamaManager.Instance.Players[cnt];
	            continue;

	        }

	        if (Vector3.Distance(GamaManager.Instance.Players[cnt].transform.position, transform.position) <
	            Vector3.Distance(closest.transform.position, transform.position))
	        {

	            if (_lastTargeted == GamaManager.Instance.Players[cnt].transform && Time.time - _lastAttackTime < 3)
	            {
                    continue;
                }

	            closest = GamaManager.Instance.Players[cnt];
	        }

	    }

	    if (closest != null)
	    {
	        SetTarget(closest);
	    }

        if (_target == null)
            return;
	    _agent.SetDestination(_target.position);

    }

    public void SetTarget(AnimalController animalController)
    {
        _target = animalController.transform;
        _agent.SetDestination(_target.position);
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

        _lastAttackTime = Time.time;
        _lastTargeted = _target;

        animal.Stun(1);
        animal.AddLife(-30 - GamaManager.Instance.Level);

        var dir = collision.transform.position - transform.position;

        animal.Rigidbody.AddForce(dir * _pushPower, ForceMode.Impulse);

        _target = null;

        Wait(true);
        _waitTimer = Time.time + 1;

        //Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        //body.velocity = pushDir * 10;
    }

    public void Wait(bool wait)
    {
        _agent.isStopped = wait;
    }
}
