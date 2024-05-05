using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private AudioSource AudioSource;
    public bool IsMuted;
    public AudioClip SelectSound;
    public AudioClip WinSound;
    public AudioClip LoseSound;
    public AudioClip MenuMusic;
    public AudioClip GameMusic;
    public AudioClip ErrorSound;

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

        AudioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(string name, float volume = 1, bool loop = true)
    {
        switch (name)
        {
            case "Menu":
                AudioSource.clip = MenuMusic;
                break;
            case "Game":
                AudioSource.clip = GameMusic;
                break;
            default:
                break;
        }

        AudioSource.volume = volume;
        AudioSource.loop = loop;
        AudioSource.Play();
    }

    public void PlaySound(string name, float volume = 1)
    {
        switch (name)
        {
            case "Select":
                AudioSource.PlayOneShot(SelectSound, volume);
                break;
            case "Win":
                AudioSource.PlayOneShot(WinSound, volume);
                break;
            case "Lose":
                AudioSource.PlayOneShot(LoseSound, volume);
                break;
            case "Error":
                AudioSource.PlayOneShot(ErrorSound, volume);
                break;
            default:
                break;
        }
    }
}
