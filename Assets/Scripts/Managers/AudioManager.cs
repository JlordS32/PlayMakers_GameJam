using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip defaultMusic;
    [SerializeField] private List<AudioClip> bgMusic;

    public static AudioManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicSource = musicSource != null ? musicSource : GetComponent<AudioSource>();
        soundSource = soundSource != null ? soundSource : GetComponent<AudioSource>();
        ApplySavedVolumes();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        PlayMusicForCurrentScene();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        musicSource = musicSource != null ? musicSource : GetComponent<AudioSource>();
        soundSource = soundSource != null ? soundSource : GetComponent<AudioSource>();

        PlayMusicForCurrentScene();
    }

    // Play music depending on the scene
    private void PlayMusicForCurrentScene()
    {
        if (musicSource == null) return;

        AudioClip sceneMusic = bgMusic.Find(x => x.name == SceneManager.GetActiveScene().name);

        // If no scene-specific music is found, use default music
        musicSource.clip = sceneMusic != null ? sceneMusic : defaultMusic;
        musicSource.Play();
    }

    private void ApplySavedVolumes()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
        float savedSoundVolume = PlayerPrefs.GetFloat("soundVolume", 1f);

        musicSource.volume = savedMusicVolume * 0.5f;
        soundSource.volume = savedSoundVolume * 1f;
    }

    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }

    public void SetSoundVolume(float newVolume)
    {
        SetSourceVolume(1f, "soundVolume", newVolume, soundSource);
    }

    public void SetMusicVolume(float newVolume)
    {
        SetSourceVolume(0.3f, "musicVolume", newVolume, musicSource);
    }

    private void SetSourceVolume(float baseVolume, string volumeName, float newVolume, AudioSource source)
    {
        if (source == null) return;

        float clampedVolume = Mathf.Clamp(newVolume, 0f, 1f);
        source.volume = clampedVolume * baseVolume;
        PlayerPrefs.SetFloat(volumeName, clampedVolume);
    }
}
