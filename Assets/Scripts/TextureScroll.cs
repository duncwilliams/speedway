using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    public float scrollSpeedY = -0.35f;
    public float scrollSpeedUpInterval = 0.05f;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Calculate the offset based on time and speed
        float offsetY = Time.time * scrollSpeedY;

        // Apply the offset to the material's main texture
        rend.material.mainTextureOffset = new Vector2(0, offsetY);
    }

    public void SpeedUpScroll()
    {
        scrollSpeedY -= scrollSpeedUpInterval;
    }
}
