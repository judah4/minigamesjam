using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Text _winnerText;

    void Start()
    {
        ShowWinners(GamaManager.Instance.Winners);
    }

    void ShowWinners(List<AnimalController> winners)
    {
        if (winners.Count < 1)
        {
            _winnerText.text = "Everyone died for the glory of the colosseum";
        }

        var winnerNames = "";
        for (int cnt = 0; cnt < winners.Count; cnt++)
        {
            winnerNames += winners[cnt].CharacterName;

            if (cnt != winners.Count - 1)
            {
                winnerNames += ", ";
            }
        }

        if (winners.Count == 1)
        {
            _winnerText.text = winnerNames + " survived the colosseum";
        }

    }
}
