using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public void Single ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Screen.orientation = ScreenOrientation.LandscapeRight;

    }

    public void Multi()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        Screen.orientation = ScreenOrientation.LandscapeRight;
    }

    public void Leave()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Screen.orientation = ScreenOrientation.Portrait;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
