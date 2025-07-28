using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class OrbGlow : MonoBehaviour
{
    public float lightRange = 2f;
    public float lightIntensity = 0.5f;

    private Renderer rend;
    private Light glowLight;

    void Start()
    {
        rend = GetComponent<Renderer>();

        glowLight = gameObject.AddComponent<Light>();
        glowLight.type = LightType.Point;
        glowLight.range = lightRange;
        glowLight.intensity = lightIntensity;
        glowLight.shadows = LightShadows.None;
    }

    void Update()
    {
        // Update light color to match orb
        if (rend != null)
            glowLight.color = rend.material.color;
    }
}
