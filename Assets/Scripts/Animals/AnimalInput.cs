using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInput : MonoBehaviour
{
    public enum Player
    {
        One = 1,
        Two,
        Three
    }

    public Player PlayerNumber = Player.One;

    public AnimalController AnimalController;

	// Use this for initialization
	void Start () {
		AnimalController.SetCharacter((int)PlayerNumber-1);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var horizInput = "Horizontal";
	    var vertInput = "Vertical";
        if (PlayerNumber > Player.One)
	    {
	        horizInput += (int)PlayerNumber;
	        vertInput += (int)PlayerNumber;
        }


        var movement = new Vector3(Input.GetAxis(horizInput), 0, Input.GetAxis(vertInput));
	    AnimalController.Move(movement);

    }
}
