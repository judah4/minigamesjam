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
	    LoadIn();


        _lion.SetTarget(GamaManager.Instance.Players[0]);

	}
	
	// Update is called once per frame
	void Update ()
	{

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

	        if (Vector3.Distance(GamaManager.Instance.Players[cnt].transform.position, _lion.transform.position) <
	            Vector3.Distance(closest.transform.position, _lion.transform.position))
	        {
	            closest = GamaManager.Instance.Players[cnt];
            }

	    }

	    if (closest != null)
	    {
	        _lion.SetTarget(closest);
        }
	}
}
