using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fruit : MonoBehaviour
{

    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Vector3 _throwDir;

	// Use this for initialization
	void Start () {
	    _rigidbody.AddForce(_throwDir, ForceMode.Impulse);

    }

    public void Throw(Vector3 throwDir)
    {
        _throwDir = throwDir;
    }
	

}
