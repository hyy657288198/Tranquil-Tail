using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Tutorial");
        PlayerPrefs.DeleteAll();
    }
    public void Resume()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("PauseMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Main()
    {
        SceneManager.LoadScene("StartMenu");
        GameObject resumeButtonObject = GameObject.Find("Resume");


    }
    public void MainMenu()
    {
        SceneManager.UnloadSceneAsync("PauseMenu");
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);

    }

    public void Charts()
    {
        SceneManager.LoadScene("Leaderboard");
    }




}
