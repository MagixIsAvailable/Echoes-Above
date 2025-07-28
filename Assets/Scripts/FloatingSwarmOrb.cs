using UnityEngine;
using System.Collections;  // Needed for IEnumerator

[RequireComponent(typeof(AudioSource))]
public class FloatingSwarmOrb : MonoBehaviour
{
    public float minSize = 0.2f;
    public float maxSize = 1.0f;
    public float floatSpeed = 1.0f;
    public float floatHeight = 0.5f;

    public Renderer orbRenderer;

    private Vector3 startPos;
    private AudioSource audioSource;
    private Color baseColor;

    void Start()
    {
        startPos = transform.position;
        audioSource = GetComponent<AudioSource>();

        // Random size
        float randomScale = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * randomScale;

        // Adjust pitch and volume depending on size
        audioSource.spatialBlend = 1f;
        audioSource.pitch = Mathf.Lerp(1.5f, 0.8f, randomScale);  // small orb = higher pitch
        audioSource.volume = Mathf.Lerp(0.2f, 0.6f, randomScale); // big orb = louder

        if (orbRenderer == null)
            orbRenderer = GetComponent<Renderer>();

        if (orbRenderer != null)
            baseColor = orbRenderer.material.color;
    }

    void Update()
    {
        // Gentle floating animation
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    /// <summary>
    /// Changes the orb’s color temporarily when a main orb is hit (smooth transition)
    /// </summary>
    public void ChangeColor(Color newColor)
    {
        if (orbRenderer == null) return;

        StopAllCoroutines(); // Stop previous transitions
        StartCoroutine(LerpColor(newColor, 0.5f)); // Smooth fade to new color

        CancelInvoke(nameof(ResetColor));
        Invoke(nameof(ResetColor), 3f);
    }

    private IEnumerator LerpColor(Color targetColor, float duration)
    {
        Color startColor = orbRenderer.material.color;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            orbRenderer.material.color = Color.Lerp(startColor, targetColor, time / duration);
            yield return null;
        }

        // Ensure final color
        orbRenderer.material.color = targetColor;
    }

    private void ResetColor()
    {
        if (orbRenderer != null)
            orbRenderer.material.color = baseColor;
    }

    /// <summary>
    /// Plays an echo sound based on the clip of the main orb
    /// </summary>
    public void PlayEcho(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip);
    }
}
