﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInput : MonoBehaviour
{

    public AnimalController AnimalController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	    AnimalController.Move(movement);

    }
}
