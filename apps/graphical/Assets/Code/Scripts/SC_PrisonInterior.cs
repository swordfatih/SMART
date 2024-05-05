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
        PlayableDirector Dig = GameObject.Find("digging_action").GetComponent<PlayableDirector>();
        PlayableDirector Isolement = GameObject.Find("isolement").GetComponent<PlayableDirector>();
        PlayableDirector Massacre = GameObject.Find("Massacre").GetComponent<PlayableDirector>();
        GameManager.Instance.Subscribe((IObserver<PlayerData>)this);
    }

    public void Notify(PlayerData playerData)
    {
        PlayerData = playerData;
        RunAnimation();
    }

    public void RunAnimation()
    {
        PlayableDirector Dig = GameObject.Find("digging_action").GetComponent<PlayableDirector>();
        PlayableDirector Isolement = GameObject.Find("isolement").GetComponent<PlayableDirector>();
        PlayableDirector Massacre = GameObject.Find("Massacre").GetComponent<PlayableDirector>();

        if (PlayerData.Player.States.Count != 0)
        {
            if (PlayerData.Player.States.Peek() is ConfinedState)
            {
                Isolement.Play();
                Isolement.stopped += (PlayableDirector source) => OnAnimationEnd(source, Isolement, "S_Isolement");
            }
            else
            {
                if (PlayerData.HasGuard && PlayerData.Player.HasDug)
                {
                    Massacre.Play();
                    Massacre.stopped += (PlayableDirector source) => OnAnimationEnd(source, Massacre, "S_Prison_Outside");
                }
                else if (PlayerData.Player.HasDug)
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
