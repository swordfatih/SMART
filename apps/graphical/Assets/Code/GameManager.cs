using UnityEngine;
using Interface;

public class GameManager : MonoBehaviour
{
    public ClientInterface Client { get; set; }
    public ServerInterface Server { get; set; }

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
