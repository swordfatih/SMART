using UnityEngine;
using Interface;
using Game;
using System.Collections.Generic;

public class GameManager : MonoBehaviour, IObservable<ServerData>, IObservable<Question>
{
    public ClientInterface Client { get; set; }
    public Server Server { get; set; }
    public bool Admin { get; set; } = false;
    public ServerData ServerData { get; set; }
    public List<IObserver<ServerData>> ServerObservers { get; set; }
    public List<IObserver<Question>> QuestionObservers { get; set; }

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
        QuestionObservers = new();
    }

    public void Update()
    {
        Instance.Client?.Handle();
    }

    public void Subscribe(IObserver<ServerData> observer)
    {
        ServerObservers.Add(observer);
    }

    public void Subscribe(IObserver<Question> observer)
    {
        QuestionObservers.Add(observer);
    }

    public void Notify(ServerData serverData)
    {
        ServerData = serverData;
        foreach (var observer in ServerObservers)
        {
            observer.Notify(ServerData);
        }
    }

    public void Notify(Question question)
    {
        foreach (var observer in QuestionObservers)
        {
            observer.Notify(question);
        }
    }
}
