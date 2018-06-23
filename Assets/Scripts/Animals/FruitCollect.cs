using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FruitEvent : UnityEvent<int>
{

}

public class FruitCollect : MonoBehaviour
{
    [SerializeField]
    private int _fruit = 0;

    public int Fruit
    {
        get { return _fruit; }
    }


    public FruitEvent OnPointsGained;


    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collide " + collision.gameObject.name);

        if (collision.transform.tag != "Fruit")
            return;
        
        //collect
        _fruit++;

        Destroy(collision.gameObject);

        if (OnPointsGained != null)
        {
            OnPointsGained.Invoke(_fruit);
        }

    }
}
