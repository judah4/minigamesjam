using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTest : MonoBehaviour {

    public List<string> scenes = new List<string>();

    //string lastLoadedScene = null;
    int lastSceneIndex = 0;

	// Use this for initialization
	void Start () {

        //Load Initial Scene
        lastSceneIndex = Random.Range(0, scenes.Count);
        string newScene = scenes[lastSceneIndex];
        SceneManager.LoadScene(newScene, LoadSceneMode.Additive);

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.R))
        {
            string oldScene = scenes[lastSceneIndex];

            lastSceneIndex = RollScenes();

            string newScene = scenes[lastSceneIndex];

            SceneManager.UnloadSceneAsync(oldScene);
            SceneManager.LoadScene(newScene, LoadSceneMode.Additive);

        }

    }

    int RollScenes()
    {
        int sceneToLoad;

        //Re-Roll scene until we get a new one. 
        do
        {
            sceneToLoad = Random.Range(0, scenes.Count);
        } while (lastSceneIndex == sceneToLoad);

        return sceneToLoad;
    }

}
