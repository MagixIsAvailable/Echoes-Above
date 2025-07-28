using UnityEngine;

public class SwarmSpawner : MonoBehaviour
{
    [Header("Swarm Settings")]
    public int orbCount = 15;           // How many orbs to spawn
    public float spawnRadius = 5f;      // Radius around center to scatter orbs
    public float minHeight = 1f;
    public float maxHeight = 3f;
    public float orbScale = 0.2f;

    [Header("Color Palette (Matches Instrument Orbs)")]
    public Color[] palette = new Color[]
    {
        new Color(0.8f, 0.5f, 1f),  // soft purple
        new Color(0.5f, 0.8f, 1f),  // light blue
        new Color(0.5f, 1f, 0.7f),  // mint green
        new Color(1f, 0.7f, 0.5f),  // peach
        new Color(1f, 0.5f, 0.6f)   // pink
    };

    void Start()
    {
        for (int i = 0; i < orbCount; i++)
        {
            // Position
            Vector3 pos = transform.position +
                          new Vector3(
                              Random.Range(-spawnRadius, spawnRadius),
                              Random.Range(minHeight, maxHeight),
                              Random.Range(-spawnRadius, spawnRadius)
                          );

            // Create sphere
            GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            orb.transform.position = pos;
            orb.transform.localScale = Vector3.one * orbScale;
            orb.transform.SetParent(this.transform);

            // Assign script
            var swarmOrb = orb.AddComponent<FloatingSwarmOrb>();

            // Create unlit material with random palette color
            Material mat = new Material(Shader.Find("Unlit/Color"));
            mat.color = palette[Random.Range(0, palette.Length)];
            orb.GetComponent<Renderer>().material = mat;

            // Disable unnecessary lighting/shadows
            var renderer = orb.GetComponent<Renderer>();
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = false;

            // Optional: Remove collider (decorative)
            Destroy(orb.GetComponent<SphereCollider>());
        }
    }
}
