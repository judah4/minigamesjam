using UnityEngine;
using System.Collections;
public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;
    void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key");
            if (!pausePanel.activeInHierarchy)
            {
                Debug.Log("not active!");

                PauseGame();
            }
            else if (pausePanel.activeInHierarchy)
            {
                ContinueGame();
            }
        }
    }
    public void PauseGame()
    {
        Debug.Log("pausing!");
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        
        //Disable scripts that still work while timescale is set to 0
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
    }
}