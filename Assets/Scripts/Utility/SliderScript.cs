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
        }
        else
        {
            Debug.LogError("Slider reference is missing!");
        }
    }

    private void LogSliderValue(float value)
    {
        if (volumeName == "musicVolume") {
            AudioManager.instance.SetMusicVolume(value);
        }

        if (volumeName == "soundVolume") {
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
