using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_JoinMenu : MonoBehaviour
{
    public TMP_InputField pseudoField;
    public TMP_InputField addressField;
    public TMP_InputField portField;
    public GameObject joinButton;

    public void ClickJoinButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        //UnityEngine.SceneManagement.SceneManager.LoadScene("startScene");
        Debug.Log(pseudoField.text);
        pseudoField.interactable = false;
        addressField.interactable = false;
        portField.interactable = false;
        joinButton.SetActive(false);

    }
}
