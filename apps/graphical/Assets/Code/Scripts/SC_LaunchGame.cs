using Network;
using Interface;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SC_LaunchGame : MonoBehaviour
{
    public TMP_InputField pseudoField;
    public TMP_InputField addressField;
    public TMP_InputField portField;
    public GameObject launchButton;

    public void ClickLaunchButton()
    {
        AudioManager.Instance.PlaySound("Select");
        var host = addressField.text;
        var port = int.Parse(portField.text);
        GameManager.Instance.Server = new Server(host, port);

        GameManager.Instance.Admin=true;

        Debug.Log("Starting server on " + host + " (" + port + ")");

        var listener = Task.Run(GameManager.Instance.Server.Listen);
        var receiver = Task.Run(GameManager.Instance.Server.Receive);

        var node = new Node(host, port);
        GameManager.Instance.Client = new ClientInterface(node, pseudoField.text);
    }
}
