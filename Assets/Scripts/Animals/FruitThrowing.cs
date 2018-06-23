using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitThrowing : MonoBehaviour
{

    [SerializeField]
    private Fruit _fruitPrefab;

    [SerializeField]
    private float _interval = 3;

    [SerializeField]
    private float _spawnHeight = 5;
    [SerializeField]
    private float _spawnRadius = 10;

    private float _time;
	// Use this for initialization
	void Start ()
	{
	    _time = Time.time + .04f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time <_time)
            return;

	    _time = Time.time + _interval + Random.Range(-2, 1);

	    var pos = RandomCircle(Vector3.zero, _spawnRadius);
	    var dir = -pos;

        pos.y = _spawnHeight;

	    dir *= Random.Range(0.4f, 1.1f);

	    var fruit = Instantiate(_fruitPrefab, pos, Quaternion.identity);
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
