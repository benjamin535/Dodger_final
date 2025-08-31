using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Go back to Main Menu
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // restart current level (useful for GameOver scene)
    public void RetryLevel()
    {
        string currentScene = PlayerPrefs.GetString("LastLevel", "Level1");
        SceneManager.LoadScene(currentScene);
    }
    // loads level 1 
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    // loads How to play 
    public void HowToPlay()
    {
        SceneManager.LoadScene("How");
    }
        // loads level 1
    public void Level_1()
    {
        SceneManager.LoadScene("Level1");
    }
        // loads level 2
    public void Level_2()
    {
        SceneManager.LoadScene("Level2");
    }
}
