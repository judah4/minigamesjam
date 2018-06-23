using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LionController : MonoBehaviour
{
    private Transform _target;


    [SerializeField] private NavMeshAgent _agent;

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
}
