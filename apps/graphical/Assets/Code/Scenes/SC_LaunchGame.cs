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
        var host = addressField.text;
        var port = int.Parse(portField.text);
        GameManager.Instance.Server = new ServerInterface(host, port);

        Debug.Log("Starting server on " + host + " (" + port + ")");

        var listener = Task.Run(GameManager.Instance.Server.Listen);
        var receiver = Task.Run(GameManager.Instance.Server.Receive);

        SceneManager.LoadScene("GameInterface", LoadSceneMode.Additive);

        var node = new Node(host, port);
        GameManager.Instance.Client = new ClientInterface(node, pseudoField.text);
    }
}
