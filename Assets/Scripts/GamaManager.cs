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

[System.Serializable]
public class AudioForLevel
{
    [SerializeField] public List<AudioClip> AudioClips;
}

public class GamaManager : MonoBehaviour
{

    public static GamaManager Instance;

    [SerializeField]
    private string _defaultScene;

    [SerializeField]
    private string _resultScene;

    [SerializeField]
    private string _pauseScene;

    public List<string> Scenes = new List<string>();

    [SerializeField]
    private List<string> loadedScenes = new List<string>();

    [SerializeField]
    private List<AnimalController> _players;

    private GameState _gameState = GameState.Wait;

    [SerializeField]
    private int _level = 0;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioForLevel> _audioClips;

    [SerializeField] private AudioClip _gamesBeginClip;

    private float _audioTime = 0;
    private int _lastClip = 0;

    public GameState GameState
    {
        get { return _gameState; }
    }

    public int Level
    {
        get { return _level; }
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
        SceneManager.LoadScene(_pauseScene, LoadSceneMode.Additive);

        //Load Initial Scene
        SceneManager.LoadScene(_defaultScene, LoadSceneMode.Additive);
        loadedScenes.Add(_defaultScene);

    }

    // Update is called once per frame
    void Update()
    {

        if (GameState == GameState.Wait)
        {
            _audioTime = Time.time + Random.Range(6f, 15f);

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _audioSource.clip = _gamesBeginClip;
                _audioSource.Play();
                LoadScene(Scenes[_level % Scenes.Count]);
                _audioTime = Time.time + Random.Range(6, 15);
            }

            for (int cnt = 0; cnt < 5; cnt++)
            {
                if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), "Alpha" + (cnt + 1))))
                {
                    LoadScene(Scenes[cnt]);
                }
            }
        }
        else if (GameState == GameState.Play)
        {
            if (Time.time > _audioTime)
            {
                _audioTime = Time.time + Random.Range(8f, 20f);
                var clips = _audioClips[_level % Scenes.Count];

                var clipIndex = Random.Range(0, clips.AudioClips.Count);
                if (clipIndex == _lastClip)
                {
                    clipIndex++;
                    clipIndex %= clips.AudioClips.Count;
                }

                _lastClip = clipIndex;

                _audioSource.clip = clips.AudioClips[clipIndex];
                _audioSource.Play();
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
        if (_gameState != GameState.End)
        {
            _gameState = GameState.Unload;
        }

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

        if (_gameState != GameState.End)
        {
            _gameState = GameState.Load;
        }

        //reload scene
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadedScenes.Add(sceneName);

        yield return new WaitForSeconds(0.5f);

        

        yield return new WaitForSeconds(1f);

        if (_gameState != GameState.End)
        {
            _gameState = GameState.Play;
        }

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
