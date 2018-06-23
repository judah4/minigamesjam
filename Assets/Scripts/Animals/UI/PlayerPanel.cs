using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{

    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _scoreText;

    public void SetPlayer(string name)
    {
        _nameText.text = name;
    }

    public void SetScore(int score)
    {
        _scoreText.text = "Fruit: " + score;
    }
}
