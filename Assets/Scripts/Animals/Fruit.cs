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

    [SerializeField]
    private float _timeSpan = 6;

    private float _startTime;

    // Use this for initialization
    void Start ()
    {

        _startTime = Time.time;


        _rigidbody.AddForce(_throwDir, ForceMode.Impulse);

    }

    void Update()
    {
        if (Time.time > _startTime + _timeSpan)
        {
            Destroy(gameObject);
        }
    }

    public void Throw(Vector3 throwDir)
    {
        _throwDir = throwDir;
    }
	

}
