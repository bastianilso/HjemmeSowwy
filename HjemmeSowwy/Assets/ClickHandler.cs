using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadHidingScene()
    {
        SceneManager.LoadSceneAsync("HidingScene");
    }

    // Update is called once per frame
    public void LoadSeekingScene()
    {
        SceneManager.LoadSceneAsync("SeekingScene");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
