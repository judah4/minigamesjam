using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamaManager : MonoBehaviour {

    private string _defaultScene;

    public List<string> Scenes = new List<string>();

    private List<string> loadedScenes = new List<string>();

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

    }

    void LoadScene(string sceneName)
    {
        for (int cnt = 0; cnt < loadedScenes.Count; cnt++)
        {
            SceneManager.UnloadSceneAsync(loadedScenes[cnt]);

            loadedScenes.Remove(sceneName);
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        loadedScenes.Add(sceneName);

    }
}
