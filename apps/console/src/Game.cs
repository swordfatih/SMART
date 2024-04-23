

using Player;

public class Game
{
    private List<Player.Player> players = [];
    public int Guard { get; set; }

    public Game()
    {

    }

    public void Init()
    {
        var randomizer = new Random();
        Guard = randomizer.Next(0, players.Count);

        for (int i = 0; i < players.Count; ++i)
        {
            players[i].Position = i;
            players[i].Team = Team.Prisoner;
            players[i].State = i == Guard ? new GuardState() : new SafeState();
        }

        var Associate = randomizer.Next(0, players.Count);
        players[Associate].Team = Team.Associate;
    }

    public void Run()
    {
        List<Action.Action> actions = [];

        // récupérer les actions
        foreach (var player in players)
        {
            Console.WriteLine("-- tour de " + player + " --");
            actions.Add(player.State.Action(player));
        }

        // executer les actions
        foreach (var action in actions)
        {
            action.Run(this);
            Console.WriteLine(action);
        }
    }

    public void AddPlayer(string name)
    {
        players.Add(new Player.Player(name));
    }

    override public string ToString()
    {
        var output = "";
        players.ForEach(x => output += x + Environment.NewLine);
        return output;
    }
}
