using UnityEngine;

public class SkyboxAnimator : MonoBehaviour
{
    public Gradient skyColor;
    public float cycleDuration = 120f; // 2 minutes for a full cycle

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (cam == null) return;

        float t = (Mathf.Sin(Time.time * (2 * Mathf.PI / cycleDuration)) + 1f) / 2f;
        cam.backgroundColor = skyColor.Evaluate(t);
    }
}
