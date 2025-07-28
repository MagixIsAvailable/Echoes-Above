using UnityEngine;

public class OrbDrift : MonoBehaviour
{
    public float driftRadius = 0.5f;
    public float driftSpeed = 0.1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float t = Time.time * driftSpeed;
        float offsetX = Mathf.PerlinNoise(t, 0f) - 0.5f;
        float offsetZ = Mathf.PerlinNoise(0f, t) - 0.5f;

        transform.position = new Vector3(
            startPos.x + offsetX * driftRadius,
            transform.position.y,
            startPos.z + offsetZ * driftRadius
        );
    }
}
