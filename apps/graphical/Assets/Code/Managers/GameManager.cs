using UnityEngine;
using Game;
using System.Collections.Generic;
using Interface;

public class GameManager : MonoBehaviour, IObservable<ServerData>, IObservable<Choice>, IObservable<Message>, IObservable<PlayerData>, IObservable<BoardData>, IObservable<UserInput>
{
    public ClientInterface Client { get; set; }
    public Server Server { get; set; }
    public bool Admin { get; set; }
    public ServerData ServerData { get; set; }
    public List<IObserver<ServerData>> ServerObservers { get; set; }
    public List<IObserver<Choice>> ChoiceObservers { get; set; }
    public List<IObserver<Message>> MessageObservers { get; set; }
    public List<IObserver<PlayerData>> PlayerObservers { get; set; }
    public List<IObserver<BoardData>> BoardObservers { get; set; }
    public List<IObserver<UserInput>> InputObservers { get; set; }
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
        Admin = false;
        Client = null;
        Server = null;
        ServerData = null;
        ServerObservers = new();
        ChoiceObservers = new();
        MessageObservers = new();
        PlayerObservers = new();
        BoardObservers = new();
        InputObservers = new();
    }

    public void Update()
    {
        Instance.Client?.Handle();
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

    public void Subscribe(IObserver<PlayerData> observer)
    {
        PlayerObservers.Add(observer);
    }

    public void Subscribe(IObserver<BoardData> observer)
    {
        BoardObservers.Add(observer);
    }

    public void Subscribe(IObserver<UserInput> observer)
    {
        InputObservers.Add(observer);
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

    public void Unsubscribe(IObserver<PlayerData> observer)
    {
        PlayerObservers.Remove(observer);
    }

    public void Unsubscribe(IObserver<BoardData> observer)
    {
        BoardObservers.Remove(observer);
    }

    public void Unsubscribe(IObserver<UserInput> observer)
    {
        InputObservers.Remove(observer);
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

    public void Notify(PlayerData playerData)
    {
        foreach (var observer in PlayerObservers)
        {
            observer.Notify(playerData);
        }
    }

    public void Notify(BoardData boardData)
    {
        foreach (var observer in BoardObservers)
        {
            observer.Notify(boardData);
        }
    }

    public void Notify(UserInput userInput)
    {
        foreach (var observer in InputObservers)
        {
            observer.Notify(userInput);
        }
    }
}
