﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamaManager : MonoBehaviour
{

    public static GamaManager Instance;

    [SerializeField]
    private string _defaultScene;

    public List<string> Scenes = new List<string>();

    [SerializeField]
    private List<string> loadedScenes = new List<string>();

    [SerializeField]
    private List<AnimalController> _players;

    public List<AnimalController> Players
    {
        get { return _players; }
    }

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {

        //Load Initial Scene
        SceneManager.LoadScene(_defaultScene, LoadSceneMode.Additive);
        loadedScenes.Add(_defaultScene);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            LoadScene(Scenes[Random.Range(0, Scenes.Count)]);

        }

        for (int cnt = 0; cnt < 5; cnt++)
        {
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode),"Alpha" + (cnt + 1))))
            {
                LoadScene(Scenes[cnt]);
            }
        }

    }

    void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));

    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        //lift characters
        for (int cnt = 0; cnt < _players.Count; cnt++)
        {
            _players[cnt].Freeze(true, _players[cnt].transform.position + new Vector3(0, 20, 0));

        }

        //unload scene
        while (loadedScenes.Count > 0)
        {
            yield return SceneManager.UnloadSceneAsync(loadedScenes[0]);

            loadedScenes.Remove(loadedScenes[0]);
        }
        
        //reload scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        loadedScenes.Add(sceneName);

        //for (int cnt = 0; cnt < _players.Count; cnt++)
        //{
        //    _players[cnt].Freeze(false);

        //}

        //var game = GameObject.FindObjectOfType<Minigame>();

        //if (game != null)
        //{
        //    for (int cnt = 0; cnt < _players.Count; cnt++)
        //    {
        //        game.Players.Add(_players[cnt]);
        //    }
        //}
    }

}
