using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup mixerGroup;
    
    [Range(0f, 1f)] public float volume;
    [Range(0f, 1f)] public float spatialBlend;
    [Range(0f, 3f)] public float pitch;
    
    public bool loop;
    public bool playOnAwake;
    [HideInInspector] public AudioSource source;
}
