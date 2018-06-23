using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TigerRun : Minigame
{
    [SerializeField]
    public LionController _lion;

	// Use this for initialization
	void Start ()
	{


	    _lion.SetTarget(GamaManager.Instance.Players[0]);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
