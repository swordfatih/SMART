using UnityEngine;
using Interface;
using System.Collections;
using System.Collections.Generic;
using Network;
using Game;

public class GameManager : MonoBehaviour
{
    public ClientInterface Client { get; set; }
    public Server Server { get; set; }
    public bool Admin { get; set; } = false;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

        // Initialisation du Game Manager...
        Client = null;
        Server = null;
    }
}
