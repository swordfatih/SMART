using Network;
using Game;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SC_Death : MonoBehaviour
{
    public void Start()
    {
        var Animation = GameObject.Find("Anim_death");

        if(Animation is not null)
        {
            var Director = Animation.GetComponent<PlayableDirector>();
            Director.stopped += (PlayableDirector source) => OnAnimationEnd(source, Director, "S_Lobby");
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
