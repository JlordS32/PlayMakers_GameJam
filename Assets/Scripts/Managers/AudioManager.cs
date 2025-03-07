using UnityEngine;

public class AudioManager : MonoBehaviour
{
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

        musicSource = GetComponent<AudioSource>();
        soundSource = transform.GetChild(0).GetComponent<AudioSource>();

        // Load saved volume levels
        ApplySavedVolumes();
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
        float clampedVolume = Mathf.Clamp(newVolume, 0f, 1f);
        source.volume = clampedVolume * baseVolume;
        PlayerPrefs.SetFloat(volumeName, clampedVolume);
    }
}
