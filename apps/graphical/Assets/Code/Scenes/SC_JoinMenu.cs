using TMPro;
using UnityEngine;
using Interface;
using Network;

public class SC_JoinMenu : MonoBehaviour
{
    public TMP_InputField pseudoField;
    public TMP_InputField addressField;
    public TMP_InputField portField;
    public GameObject joinButton;
    private GameManager GameManager;

    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }

    public void ClickJoinButton()
    {
        var node = new Node(addressField.text, int.Parse(portField.text));
        GameManager.Client = new ClientInterface(node, pseudoField.text);
    }
}
