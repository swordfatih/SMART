using Network;
using Game;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SC_Death : MonoBehaviour
{
    public void Update()
    {
        var Animation = GameObject.Find("Anim_death");

        if (Animation != null)
        {
            var director = Animation.GetComponent<PlayableDirector>();

            if (director.state == PlayState.Paused)
            {
                SceneManager.LoadScene("S_Lobby");
            }
        }
    }
}
