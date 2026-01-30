using System;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public Sound[] sounds;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(string soundName)
    {
        Sound sound = Array.Find(sounds, s => s.name == soundName);
        if (sound != null)
        {
            audioSource.volume = sound.volume;
            
            // if pitch is set to 0, randomize it slightly for variety
            if (sound.pitch == 0f)
            {
                audioSource.pitch = UnityEngine.Random.Range(0.85f, 1.05f);
            }
            else
            {
                audioSource.pitch = sound.pitch;
            }
            
            audioSource.PlayOneShot(sound.clip);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }
}

[Serializable] public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume = 1.0f;
    public float pitch = 1.0f;
}
