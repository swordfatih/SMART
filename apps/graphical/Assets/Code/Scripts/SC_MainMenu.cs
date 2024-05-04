using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class SC_MainMenu : MonoBehaviour
{
    public GameObject inputField;
    public GameObject mainMenu;
    public GameObject joinMenu;
    public GameObject startGameMenu;
    public TMP_Text Text_ip;

    public void Start()
    {
        MainMenuButton();
        Text_ip.text = GetLocalIPAddress();
    }

    public void StartGameMenuButton()
    {
        inputField.SetActive(true);
        mainMenu.SetActive(false);
        joinMenu.SetActive(false);
        startGameMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        inputField.SetActive(false);
        mainMenu.SetActive(true);
        joinMenu.SetActive(false);
        startGameMenu.SetActive(false);
    }

    public void JoinMenuButton()
    {
        inputField.SetActive(true);
        mainMenu.SetActive(false);
        joinMenu.SetActive(true);
        startGameMenu.SetActive(false);
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
