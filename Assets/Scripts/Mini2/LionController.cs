using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LionController : MonoBehaviour
{
    private Transform _target;


    [SerializeField] private NavMeshAgent _agent;

    [SerializeField]
    private float _pushPower = 9;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if(_target == null)
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

        animal.Stun(1);

        var dir = collision.transform.position - transform.position;

        animal.Rigidbody.AddForce(dir * _pushPower, ForceMode.Impulse);

        //Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        //body.velocity = pushDir * 10;
    }

    public void Wait(bool wait)
    {
        _agent.isStopped = wait;
    }
}
