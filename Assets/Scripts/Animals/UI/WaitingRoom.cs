using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoom : MonoBehaviour
{
    [SerializeField]
    private GameObject controllerControls;

    [SerializeField]
    private GameObject keyboardControls;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleControllerTut()
    {
        controllerControls.SetActive(!controllerControls.activeInHierarchy);
    }

    public void ToggleKeyboardTut()
    {
        keyboardControls.SetActive(!keyboardControls.activeInHierarchy);
    }
}
