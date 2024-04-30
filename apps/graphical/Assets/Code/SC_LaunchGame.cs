using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SC_LaunchGame : MonoBehaviour
{
    public TMP_InputField pseudoField;
    public TMP_InputField addressField;
    public TMP_InputField portField;
    public GameObject launchButton;
    // Start is called before the first frame update
    public void ClickLaunchButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        //UnityEngine.SceneManagement.SceneManager.LoadScene("startScene");
        Debug.Log(pseudoField.text);
        pseudoField.interactable = false;
        addressField.interactable = false;
        portField.interactable = false;
        launchButton.SetActive(false);
    }
}
