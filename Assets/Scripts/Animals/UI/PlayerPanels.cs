using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanels : MonoBehaviour
{

    [SerializeField] private PlayerPanel _panelPrefab;

    [SerializeField] private RectTransform _parentPanel;

    [SerializeField] private List<PlayerPanel> _panels = new List<PlayerPanel>();

    [SerializeField] private List<AnimalController> _players = new List<AnimalController>();
    // Use this for initialization
    void Start () {
        for (int cnt = 0; cnt < _players.Count; cnt++)
        {
            var panel = Instantiate(_panelPrefab, _parentPanel);
            _panels.Add(panel);

            panel.SetPlayer("Player " + (cnt+1));

            var collect = _players[cnt].GetComponent<FruitCollect>();
            collect.OnPointsGained.AddListener(panel.SetScore);

        }
	}
	
}
