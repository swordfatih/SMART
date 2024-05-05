using Network;
using Game;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SC_PrisonInterior : MonoBehaviour, IObserver<PlayerData>
{

    public PlayerData PlayerData { get; set; }
    public PlayableDirector Guard;
    public PlayableDirector Dig;
    public PlayableDirector Isolement;
    public PlayableDirector Massacre;
    
    
    public void Start()
    {
        GameManager.Instance.Subscribe((IObserver<PlayerData>)this);
    }
    
    public void Notify(PlayerData playerData)
    {
        PlayerData = playerData;
        Update();
    }

    public void Update()
    {
        RunAnimation();
    }
    
    public void RunAnimation()
    {
         if (PlayerData.Player.States.Count != 0)
          {
            if(PlayerData.Player.States.Peek() is ConfinedState)
            {
                //SceneManager.LoadScene("S_Isolement");
                Debug.Log("You are confined, you can't play.");
            } else {
                if (PlayerData.HasGuard && PlayerData.Player.HasDug)
                {
                  Massacre.Play();  
                }
                else
                {
                    Dig.Play();
                }
            }
          }
    }
    public void OnAnimationEnd()
        {
            SceneManager.LoadScene("S_Isolement");
        }
}
