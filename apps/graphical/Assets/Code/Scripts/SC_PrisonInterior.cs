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
        Isolement.Play();
        Isolement.stopped += (PlayableDirector source) => OnAnimationEnd(source, Isolement, "S_Isolation");
        Massacre.stopped += (PlayableDirector source) => OnAnimationEnd(source, Massacre, "S_Prison_Outside"); 
    }
    
    public void Notify(PlayerData playerData)
    {
        PlayerData = playerData;
        RunAnimation();
    }
    
    public void RunAnimation()
    {
         if (PlayerData.Player.States.Count != 0)
          {
            if(PlayerData.Player.States.Peek() is ConfinedState)
            {
                Isolement.Play();  
                
            } else {
                if (PlayerData.HasGuard && PlayerData.Player.HasDug)
                {
                    Massacre.Play();
                }
                else if(PlayerData.Player.HasDug)
                {
                    Dig.Play();
                }
            }
          }
    }
    void OnAnimationEnd(PlayableDirector source, PlayableDirector director, string scene)
    {
        if (source == director)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
