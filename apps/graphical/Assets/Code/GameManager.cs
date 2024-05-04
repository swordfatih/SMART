using UnityEngine;
using Interface;
using Game;
using System.Collections.Generic;

public class GameManager : MonoBehaviour, IObservable<ServerData>
{
    public ClientInterface Client { get; set; }
    public Server Server { get; set; }
    public bool Admin { get; set; } = false;
    public ServerData ServerData { get; set; }
    public List<IObserver<ServerData>> Observers { get; set; }

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
        ServerData = null;
        Observers = new();
    }

    public void Update()
    {
        Instance.Client?.Handle();
    }

    public void Subscribe(IObserver<ServerData> observer)
    {
        Debug.Log("Subscribing observer...");
        Observers.Add(observer);
    }

    public void Notify()
    {
        foreach (var observer in Observers)
        {
            observer.Notify(ServerData);
        }
    }
}
