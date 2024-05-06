using Network;
using Game;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SC_PrisonInterior : MonoBehaviour, IObserver<PlayerData>
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
        var dig_animation = GameObject.Find("digging_action");
        var isolement_animation = GameObject.Find("Envoi_gardien");
        var massacre_animation = GameObject.Find("Massacre");
        
        if (dig_animation is not null && isolement_animation is not null && massacre_animation is not null)
        {
            PlayableDirector Dig = dig_animation.GetComponent<PlayableDirector>();
            PlayableDirector Isolement = isolement_animation.GetComponent<PlayableDirector>();
            PlayableDirector Massacre = massacre_animation.GetComponent<PlayableDirector>();
            if (PlayerData.Player.Status == Status.Dead)
            {
                Massacre.Play();
                Massacre.stopped += (PlayableDirector source) => OnAnimationEnd(source, Massacre, "S_death");
            }
            if (PlayerData.Player.States.Count != 0)
            {
                if (PlayerData.Player.States.Peek() is ConfinedState)
                {
                    Isolement.Play();
                    Isolement.stopped += (PlayableDirector source) => OnAnimationEnd(source, Isolement, "S_Isolement");
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
