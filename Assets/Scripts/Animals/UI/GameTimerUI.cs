using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerUI : MonoBehaviour
{

    [SerializeField]
    private AudioSource _countDownSource;
    [SerializeField]
    private Text _text;

    public void TimeUpdate(float gameTime)
    {

        if (gameTime < 3.2f && _countDownSource.isPlaying == false)
        {
            _countDownSource.Play();
        }

        if (gameTime < 0)
        {
            _text.text = "0:00";
        }

        var minute = (int)(gameTime / 60);
        var second = (int) (gameTime % 60);

        _text.text = minute + ":" + second.ToString().PadLeft(2, '0');

    }

}
