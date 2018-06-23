using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamaManager : MonoBehaviour {

    [SerializeField]
    private string _defaultScene;

    public List<string> Scenes = new List<string>();

    [SerializeField]
    private List<string> loadedScenes = new List<string>();

    private List<AnimalController> _players;

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

        if (Input.GetKeyUp(KeyCode.R))
        {
            LoadScene(Scenes[Random.Range(0, Scenes.Count)]);

        }

        for (int cnt = 0; cnt < 5; cnt++)
        {
            if (Input.GetKeyDown("Alpha" + (cnt + 1)))
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
        while (loadedScenes.Count > 1)
        {
            yield return SceneManager.UnloadSceneAsync(loadedScenes[0]);

            loadedScenes.Remove(loadedScenes[0]);
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        loadedScenes.Add(sceneName);

        var game = GameObject.FindObjectOfType<Minigame>();

        if (game != null)
        {
            for (int cnt = 0; cnt < _players.Count; cnt++)
            {
                game.Players.Add(_players[cnt]);
            }
        }
    }

}
