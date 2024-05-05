using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class SC_MainMenu : MonoBehaviour
{
    public GameObject IT_Form;
    public GameObject IT_MainMenu;
    public GameObject IT_ClientMenu;
    public GameObject IT_ServerMenu;
    public TMP_InputField IT_HostInput;

    public void Start()
    {
        AudioManager.Instance.PlayMusic("Menu", 0.2f);
        MainMenuButton();
        IT_HostInput.text = GetLocalIPAddress();
    }

    public void ServerMenuButton()
    {
        AudioManager.Instance.PlaySound("Select");
        IT_Form.SetActive(true);
        IT_MainMenu.SetActive(false);
        IT_ClientMenu.SetActive(false);
        IT_ServerMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        AudioManager.Instance.PlaySound("Select");
        IT_Form.SetActive(false);
        IT_MainMenu.SetActive(true);
        IT_ClientMenu.SetActive(false);
        IT_ServerMenu.SetActive(false);
    }

    public void ClientMenuButton()
    {
        AudioManager.Instance.PlaySound("Select");
        IT_Form.SetActive(true);
        IT_MainMenu.SetActive(false);
        IT_ClientMenu.SetActive(true);
        IT_ServerMenu.SetActive(false);
    }

    public void QuitButton()
    {
        AudioManager.Instance.PlaySound("Select");
        Application.Quit();
    }

    public string GetLocalIPAddress()
    {
        // Obtient l'adresse IP de l'ordinateur local
        string ipAddress = "";

        // Trouve toutes les adresses IPv4 associées à cet ordinateur
        IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

        // Parcourt toutes les adresses pour trouver une adresse IPv4
        foreach (IPAddress ip in localIPs)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipAddress = ip.ToString();
                break;
            }
        }

        return ipAddress;
    }
}
