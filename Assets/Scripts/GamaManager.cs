using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Wait,
    Play,
    Unload,
    Load,
    End
}

public class GamaManager : MonoBehaviour
{

    public static GamaManager Instance;

    [SerializeField]
    private string _defaultScene;

    [SerializeField]
    private string _resultScene;

    public List<string> Scenes = new List<string>();

    [SerializeField]
    private List<string> loadedScenes = new List<string>();

    [SerializeField]
    private List<AnimalController> _players;

    private GameState _gameState = GameState.Wait;

    [SerializeField]
    private int _level = 0;

    public GameState GameState
    {
        get { return _gameState; }
    }

    public List<AnimalController> Players
    {
        get { return _players; }
    }

    public List<AnimalController> Winners = new List<AnimalController>();

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
        if (GameState == GameState.Wait)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                LoadScene(Scenes[_level % Scenes.Count]);

            }

            for (int cnt = 0; cnt < 5; cnt++)
            {
                if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), "Alpha" + (cnt + 1))))
                {
                    LoadScene(Scenes[cnt]);
                }
            }
        }
    }

    public void LoadNextLevel()
    {
        _level++;
        LoadScene(Scenes[_level % Scenes.Count]);
    }

    void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));

    }

    IEnumerator LoadSceneAsync(string sceneName)
    {

        _gameState = GameState.Unload;
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

        _gameState = GameState.Load;

        //reload scene
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadedScenes.Add(sceneName);

        yield return new WaitForSeconds(0.5f);

        

        yield return new WaitForSeconds(1f);

        _gameState = GameState.Play;

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

    public void FinishGame(AnimalController alivePlayer)
    {
        Winners.Add(alivePlayer);
        _gameState = GameState.End;
        LoadScene(_resultScene);
    }
}
