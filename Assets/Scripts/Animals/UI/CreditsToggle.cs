using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject _creditsPanel;

	// Use this for initialization
	void Start () {
	    _creditsPanel.SetActive(false);

    }

    public void Toggle()
    {
        _creditsPanel.SetActive(!_creditsPanel.activeInHierarchy);

    }


}
