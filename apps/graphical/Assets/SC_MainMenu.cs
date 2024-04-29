using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject JoinMenu;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton();
    }

    public void LaunchButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        UnityEngine.SceneManagement.SceneManager.LoadScene("startScene");
    }

    public void MainMenuButton()
    {
        // Show Main Menu
        MainMenu.SetActive(true);
        JoinMenu.SetActive(false);
    }

    public void JoinButton()
    {
        MainMenu.SetActive(false);
        JoinMenu.SetActive(true);
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}