using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System;
using TMPro;

public class Textscript : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text Text_ip;
    void Start()        
    {
        Text_ip.text = GetLocalIPAddress();
    }
    
    string GetLocalIPAddress()
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