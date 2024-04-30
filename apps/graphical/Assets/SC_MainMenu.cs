using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject joinMenu;
    public GameObject startGameMenu;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void LaunchButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        //UnityEngine.SceneManagement.SceneManager.LoadScene("startScene");
        mainMenu.SetActive(false);
        joinMenu.SetActive(false);
        startGameMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        // Show Main Menu
        mainMenu.SetActive(true);
        joinMenu.SetActive(false);
        startGameMenu.SetActive(false);
    }

    public void JoinButton()
    {
        mainMenu.SetActive(false);
        joinMenu.SetActive(true);
        startGameMenu.SetActive(false);
        Debug.Log("Bouton cliqué!");
        gameManager.globalScore += 1;
        Debug.Log(gameManager.globalScore);

    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}