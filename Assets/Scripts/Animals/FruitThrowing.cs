﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TimerEvent : UnityEvent<float>
{

}

public abstract class Minigame : MonoBehaviour
{
    //[SerializeField] protected List<AnimalController> _players = new List<AnimalController>();

    //public List<AnimalController> Players
    //{
    //    get { return _players; }
    //}

    [SerializeField]
    protected float _matchTimer = 40;

    public TimerEvent OnMatchTimer;

    public List<Transform> Spawns = new List<Transform>();

    private bool _calledMatchOver = false;

    protected void LoadIn()
    {
        StartCoroutine(LoadAsync());
        
    }

    IEnumerator LoadAsync()
    {
        while (GamaManager.Instance.GameState != GameState.Play)
        {
            yield return 0;
        }

        for (int cnt = 0; cnt < GamaManager.Instance.Players.Count; cnt++)
        {
            GamaManager.Instance.Players[cnt].Freeze(false, Spawns[cnt].position);

        }
    }

    protected void MatchOver()
    {
        if(_calledMatchOver)
            return;

        _calledMatchOver = true;
        GamaManager.Instance.LoadNextLevel();
    }

}

public class FruitThrowing : Minigame
{

    [SerializeField]
    private Fruit[] _fruitPrefabs;

    [SerializeField]
    private float _interval = 3;

    [SerializeField]
    private Vector2 _spawnHeightRange = new Vector2(10,20);
    [SerializeField]
    private float _spawnRadius = 10;

    private float _time;

    


    

    // Use this for initialization
    void Start ()
    {
        LoadIn();

        _time = Time.time + .04f;

        OnMatchTimer.Invoke(_matchTimer);

    }

    // Update is called once per frame
    void Update ()
	{

        if(GamaManager.Instance.GameState != GameState.Play)
            return;

	    _matchTimer -= Time.deltaTime;
	    OnMatchTimer.Invoke(_matchTimer);

	    if (_matchTimer < 0)
	    {
            MatchOver();
	    }

        if (Time.time <_time)
            return;

	    _time = Time.time + _interval + Random.Range(-2, 1);

	    var pos = RandomCircle(Vector3.zero, _spawnRadius);
	    var dir = -pos;

        pos.y = Random.Range(_spawnHeightRange.x, _spawnHeightRange.y);

	    dir *= Random.Range(0.4f, 1.1f);

	    var fruit = Instantiate(_fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)], pos, Quaternion.identity, transform);
        fruit.Throw(dir);
	}

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }

}
