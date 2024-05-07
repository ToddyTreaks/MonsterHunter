using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private string groupName; // Example: "MusicVolume"
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value)
    {
        SetVolume(groupName, value);
    }

    public void SetVolume(string groupName, float volume)
    {
        masterMixer.SetFloat(groupName, Mathf.Log10(volume) * 20);
    }
}