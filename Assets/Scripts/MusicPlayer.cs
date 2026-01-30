using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;
    
    void Awake()
    {
        // check if the music player already exists
        if (instance != null && instance != this)
        {
            // if it does exist, destroy the new one
            Destroy(this.gameObject);
            return;
        }

        // if it doesn't exist, set this as the instance and keep it alive between scenes
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
