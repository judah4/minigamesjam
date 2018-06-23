using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TimerEvent : UnityEvent<float>
{

}

public abstract class Minigame : MonoBehaviour
{
    [SerializeField] protected List<AnimalController> _players = new List<AnimalController>();

    public List<AnimalController> Players
    {
        get { return _players; }
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

    [SerializeField]
    private float _matchTimer = 80;

    public TimerEvent OnMatchTimer;


    

    // Use this for initialization
    void Start ()
	{
	    _time = Time.time + .04f;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _matchTimer -= Time.deltaTime;
	    OnMatchTimer.Invoke(_matchTimer);

        if (Time.time <_time)
            return;

	    _time = Time.time + _interval + Random.Range(-2, 1);

	    var pos = RandomCircle(Vector3.zero, _spawnRadius);
	    var dir = -pos;

        pos.y = Random.Range(_spawnHeightRange.x, _spawnHeightRange.y);

	    dir *= Random.Range(0.4f, 1.1f);

	    var fruit = Instantiate(_fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)], pos, Quaternion.identity);
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
