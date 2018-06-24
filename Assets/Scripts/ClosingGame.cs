using UnityEngine;
using System.Collections;

public class ClosingGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Quit();

    }

    public void Quit()
    {
        Application.Quit();
    }
}
