using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioQueue : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private List<AudioClip> _audioClips;
    [Header("Internal")]
    [SerializeField]
    private int _index = 0;

	// Use this for initialization
	void Start ()
	{
	    _audioSource.clip = _audioClips[_index];
	    _audioSource.Play();

    }

    // Update is called once per frame
    void Update () {
	    if (_audioSource.isPlaying == false)
	    {
	        TriggerNext();
	    }
	}

    void TriggerNext()
    {
        if (_audioClips.Count == _index + 1)
        {
            //loop
            _audioSource.clip = _audioClips[_index];
            _audioSource.loop = true;
            
        }
        else
        {
            _index++;
        }

        _audioSource.clip = _audioClips[_index];
        _audioSource.Play();
    }
}
