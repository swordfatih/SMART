using Network;
using Interface;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_JoinMenu : MonoBehaviour
{
    public TMP_InputField IT_NameInput;
    public TMP_InputField IT_HostInput;
    public TMP_InputField IT_PortInput;

    public void ClickJoinButton()
    {
        AudioManager.Instance.PlaySound("Select");
        var host = IT_HostInput.text;
        var port = int.Parse(IT_PortInput.text);

        var node = new Node(host, port);
        GameManager.Instance.Client = new ClientInterface(node, IT_NameInput.text);;
    }
}
