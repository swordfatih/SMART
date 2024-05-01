using Network;
using Interface;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;

public class SC_LaunchGame : MonoBehaviour
{
    public TMP_InputField pseudoField;
    public TMP_InputField addressField;
    public TMP_InputField portField;
    public GameObject launchButton;
    private GameManager GameManager;

    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }

    public void ClickLaunchButton()
    {
        var host = addressField.text;
        var port = int.Parse(portField.text);
        GameManager.Server = new ServerInterface(host, port);

        Debug.Log("Starting server on " + host + " (" + port + ")");

        var listener = Task.Run(GameManager.Server.Listen);
        var receiver = Task.Run(GameManager.Server.Receive);

        var node = new Node(host, port);
        GameManager.Client = new ClientInterface(node, pseudoField.text);
    }
}
