using Network;
using Game;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SC_Isolement : MonoBehaviour, IObserver<PlayerData>
{

    public PlayerData PlayerData { get; set; }

    public void Start()
    {
        GameManager.Instance.Subscribe(this);
    }

    public void Notify(PlayerData playerData)
    {
        PlayerData = playerData;
        
        if(PlayerData.Player.Status == Status.Alive)
        {
            SceneManager.LoadScene("S_PrisonInterior");
        }
    }
}
