using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AnimalController : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;

    private Vector3 _movement;

	// Use this for initialization
    void Start()
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Move(Vector3 movement)
    {
        _movement = movement;
        _movement.y = 0;
    }
}
