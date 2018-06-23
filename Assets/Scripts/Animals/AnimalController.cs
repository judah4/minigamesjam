using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AnimalController : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private Vector3 _movement;

    [SerializeField]
    private float _moveSpeed = 5;

    [SerializeField]
    private int life = 10;

    public bool Dead
    {
        get { return life < 1; }
    }

    // Use this for initialization
    void Start()
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
        if(Dead)
            return;

	    _characterController.Move(_movement * _moveSpeed * Time.deltaTime);

	    if (_movement.magnitude > 0)
	    {
	        var rot = Quaternion.LookRotation(_movement);
	        _characterController.transform.rotation = Quaternion.Lerp(_characterController.transform.rotation, rot, 10 * Time.deltaTime);
	    }
	}

    public void Move(Vector3 movement)
    {
        _movement = movement;
        _movement.y = 0;
    }
}
