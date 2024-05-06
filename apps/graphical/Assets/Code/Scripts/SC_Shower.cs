using Network;
using Game;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SC_Shower : MonoBehaviour, IObserver<PlayerData>
{

    public PlayerData PlayerData { get; set; }

    public void Start()
    {
        GameManager.Instance.Subscribe((IObserver<PlayerData>)this);
    }

    public void Notify(PlayerData playerData)
    {
        PlayerData = playerData;
        RunAnimation();
    }

    public void RunAnimation()
    {
            if (PlayerData.Player.Status == Status.Dead)
            {
                SceneManager.LoadScene("S_death");
            }
    }
    
}