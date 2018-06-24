using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TigerRun : Minigame
{
    [SerializeField]
    public LionController _lion;

    [SerializeField]
    public List<LionController> _spareLions;

    // Use this for initialization
    void Start ()
	{
	    LoadIn();


        _lion.SetTarget(GamaManager.Instance.Players[0]);

	    OnMatchTimer.Invoke(_matchTimer);


	    if (GamaManager.Instance.Level > 5)
	    {
	        var otherLion = Instantiate(_lion, _lion.transform.position + new Vector3(2, 0, 2), Quaternion.identity, transform);
            _spareLions.Add(otherLion);
	    }


    }

    // Update is called once per frame
    void Update ()
	{

	    _matchTimer -= Time.deltaTime;
	    OnMatchTimer.Invoke(_matchTimer);

	    if (_matchTimer < 0)
	    {
	        MatchOver();
	    }

	    var wait = GamaManager.Instance.GameState != GameState.Play;
        _lion.Wait(wait);
	    for (int cnt = 0; cnt < _spareLions.Count; cnt++)
	    {
	        _spareLions[cnt].Wait(wait);
	    }

	    int numAlive = 0;
	    AnimalController alivePlayer = null;

	    for (int cnt = 0; cnt < GamaManager.Instance.Players.Count; cnt++)
	    {
	        if (GamaManager.Instance.Players[cnt].Dead)
	        {
                continue;
	        }
	        numAlive++;
	        alivePlayer = GamaManager.Instance.Players[cnt];

	    }

	    if (numAlive <= 1)
	    {
	        GamaManager.Instance.FinishGame(alivePlayer);
	    }

	    
	}
}
