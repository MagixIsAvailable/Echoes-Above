using UnityEngine;

public class BackgroundColorController : MonoBehaviour
{
    public Camera cam;                   // Drag CenterEyeAnchor camera here
    public float transitionSpeed = 0.5f; // Speed of color transition
    public bool autoFadeBack = true;     // If true, fades back to dark after a delay
    public float fadeDelay = 5f;         // Time to wait before starting fade back
    public float fadeBackSpeed = 0.1f;   // How fast to fade back

    private Color targetColor;
    private Color darkColor = new Color(0.05f, 0.05f, 0.05f);
    private float lastOrbTime;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        targetColor = darkColor;
        cam.backgroundColor = targetColor;
    }

    void Update()
    {
        // Smooth transition to target color
        cam.backgroundColor = Color.Lerp(
            cam.backgroundColor,
            targetColor,
            Time.deltaTime * transitionSpeed
        );

        // Auto-fade back to dark if enabled
        if (autoFadeBack && Time.time - lastOrbTime > fadeDelay)
        {
            targetColor = Color.Lerp(targetColor, darkColor, Time.deltaTime * fadeBackSpeed);
        }
    }

    public void SetNewBackgroundColor(Color orbColor)
    {
        // Mix orb color with dark so it's never too bright
        targetColor = Color.Lerp(darkColor, orbColor, 0.5f);
        lastOrbTime = Time.time;
    }
}
