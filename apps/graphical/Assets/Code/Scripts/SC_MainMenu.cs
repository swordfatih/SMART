using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_MainMenu : MonoBehaviour
{
    public GameObject IT_Form;
    public GameObject IT_MainMenu;
    public GameObject IT_ClientMenu;
    public GameObject IT_ServerMenu;
    public TMP_InputField IT_HostInput;

    public void Start()
    {
        MainMenuButton();
        IT_HostInput.text = GetLocalIPAddress();
    }

    public void ServerMenuButton()
    {
        IT_Form.SetActive(true);
        IT_MainMenu.SetActive(false);
        IT_ClientMenu.SetActive(false);
        IT_ServerMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        IT_Form.SetActive(false);
        IT_MainMenu.SetActive(true);
        IT_ClientMenu.SetActive(false);
        IT_ServerMenu.SetActive(false);
    }

    public void ClientMenuButton()
    {
        IT_Form.SetActive(true);
        IT_MainMenu.SetActive(false);
        IT_ClientMenu.SetActive(true);
        IT_ServerMenu.SetActive(false);
    }

    public void QuitButton()
    {
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
