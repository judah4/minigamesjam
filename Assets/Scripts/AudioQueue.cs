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

    [SerializeField] private bool _noLoop;
    [Header("Internal")]
    [SerializeField]
    private int _index = 0;
    [SerializeField]
    private bool playedLast = false;


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
            playedLast = true;

            if (_noLoop)
            {
                return;
            }


            //loop
            _audioSource.loop = !_noLoop;

        }
        else
        {
            _index++;
        }


        _audioSource.clip = _audioClips[_index];
        _audioSource.Play();
    }
}
