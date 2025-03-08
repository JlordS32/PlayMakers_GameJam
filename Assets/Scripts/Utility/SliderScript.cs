using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private string volumeName;

    void Start()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(LogSliderValue);
            
            // Set slider to saved volume
            if (volumeName == "musicVolume")
            {
                slider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
            }
            else if (volumeName == "soundVolume")
            {
                slider.value = PlayerPrefs.GetFloat("soundVolume", 1f);
            }
        }
        else
        {
            Debug.LogError("Slider reference is missing!");
        }
    }

    private void LogSliderValue(float value)
    {
        if (volumeName == "musicVolume")
        {
            AudioManager.instance.SetMusicVolume(value);
        }
        else if (volumeName == "soundVolume")
        {
            AudioManager.instance.SetSoundVolume(value);
        }
    }

    void OnDestroy()
    {
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(LogSliderValue);
        }
    }
}
