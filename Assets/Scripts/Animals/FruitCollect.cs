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

    [SerializeField] private AnimalController _animal;



    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collide " + collision.gameObject.name);

        if (collision.transform.tag != "Fruit")
            return;

        var fruit = collision.gameObject.GetComponent<Fruit>();

        var healAmt = 1;
        if (fruit != null)
        {
            healAmt = fruit.HealAmt;
        }

        //collect
        _animal.AddLife(healAmt);

        _animal.PlayEatEffect();

        Destroy(collision.gameObject);

    }
}
