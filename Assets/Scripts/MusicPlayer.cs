using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer Instance;
    
    void Awake()
    {
        // check if the music player already exists
        if (Instance != null && Instance != this)
        {
            // if it does exist, destroy the new one
            Destroy(this.gameObject);
            return;
        }

        // if it doesn't exist, set this as the instance and keep it alive between scenes
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
