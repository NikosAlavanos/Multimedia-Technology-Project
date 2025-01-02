using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour
{
    public GameObject deathMenuManager;

    void Start()
    {
        deathMenuManager.SetActive(false);
    }

    public void ShowDeathMenuManager()
    {
        Debug.Log("ShowDeathMenu Called");
        deathMenuManager.SetActive(true);
        Time.timeScale = 0f; // Pause the game when the menu is displayed
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Ensure time is resumed before quitting
        Application.Quit();
    }
}