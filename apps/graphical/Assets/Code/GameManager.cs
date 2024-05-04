using UnityEngine;
using Interface;
using Game;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IObservable<ServerData>, IObservable<Choice>, IObservable<Message>
{
    public ClientInterface Client { get; set; }
    public Server Server { get; set; }
    public bool Admin { get; set; } = false;
    public ServerData ServerData { get; set; }
    public List<IObserver<ServerData>> ServerObservers { get; set; }
    public List<IObserver<Choice>> ChoiceObservers { get; set; }
    public List<IObserver<Message>> MessageObservers { get; set; }

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
        ServerObservers = new();
        ChoiceObservers = new();
        MessageObservers = new();
    }

    public void Update()
    {
        Instance.Client?.Handle();
    }

    public void Disconnect()
    {
        Client = null;
        SceneManager.LoadScene("GameMenu");
    }

    public void Subscribe(IObserver<ServerData> observer)
    {
        ServerObservers.Add(observer);
    }

    public void Subscribe(IObserver<Choice> observer)
    {
        ChoiceObservers.Add(observer);
    }

    public void Subscribe(IObserver<Message> observer)
    {
        MessageObservers.Add(observer);
    }

    public void Unsubscribe(IObserver<ServerData> observer)
    {
        ServerObservers.Remove(observer);
    }

    public void Unsubscribe(IObserver<Choice> observer)
    {
        ChoiceObservers.Remove(observer);
    }

    public void Unsubscribe(IObserver<Message> observer)
    {
        MessageObservers.Remove(observer);
    }

    public void Notify(ServerData serverData)
    {
        ServerData = serverData;
        foreach (var observer in ServerObservers)
        {
            observer.Notify(ServerData);
        }
    }

    public void Notify(Choice choice)
    {
        foreach (var observer in ChoiceObservers)
        {
            observer.Notify(choice);
        }
    }

    public void Notify(Message message)
    {
        foreach (var observer in MessageObservers)
        {
            observer.Notify(message);
        }
    }
}
