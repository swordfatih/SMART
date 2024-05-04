using Network;
using Interface;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_JoinMenu : MonoBehaviour
{
    public TMP_InputField pseudoField;
    public TMP_InputField addressField;
    public TMP_InputField portField;
    public GameObject joinButton;

    public void ClickJoinButton()
    {
        var host = addressField.text;
        var port = int.Parse(portField.text);

        var node = new Node(host, port);
        GameManager.Instance.Client = new ClientInterface(node, pseudoField.text);;

        SceneManager.LoadScene("GameLobby");
    }
}
