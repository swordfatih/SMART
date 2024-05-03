using UnityEngine;

public class SC_MainMenu : MonoBehaviour
{
    public GameObject inputField;
    public GameObject mainMenu;
    public GameObject joinMenu;
    public GameObject startGameMenu;

    public void Start()
    {
        MainMenuButton();
    }

    public void StartGameMenuButton()
    {
        inputField.SetActive(true);
        mainMenu.SetActive(false);
        joinMenu.SetActive(false);
        startGameMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        inputField.SetActive(false);
        mainMenu.SetActive(true);
        joinMenu.SetActive(false);
        startGameMenu.SetActive(false);
    }

    public void JoinMenuButton()
    {
        inputField.SetActive(true);
        mainMenu.SetActive(false);
        joinMenu.SetActive(true);
        startGameMenu.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}