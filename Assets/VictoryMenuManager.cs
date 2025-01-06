using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryMenuManager : MonoBehaviour
{
    public GameObject victoryMenu; // Reference to the victory menu panel

    void Start()
    {
        victoryMenu.SetActive(false); // Hide the menu at the start
    }

    public void ShowVictoryMenu()
    {
        Debug.Log("Victory Achieved!");
        victoryMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game when the menu is displayed
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game before going to the main menu
        SceneManager.LoadScene("MainMenu");
    }
}