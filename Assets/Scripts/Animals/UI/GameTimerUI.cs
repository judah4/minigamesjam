using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerUI : MonoBehaviour
{

    [SerializeField]
    private Text _text;

    public void TimeUpdate(float gameTime)
    {
        if (gameTime < 0)
        {
            _text.text = "0:00";
        }

        var minute = (int)(gameTime / 60);
        var second = (int) (gameTime % 60);

        _text.text = minute + ":" + second.ToString().PadLeft(2, '0');

    }

}
