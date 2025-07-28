using UnityEngine;

public class OrbInstrument : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public AudioSource note;          // The AudioSource on this orb
    public Renderer orbRenderer;      // Mesh Renderer of the orb (for glow)

    private Color baseEmissionColor;

    void Start()
    {
        // Store the original emission color so we can reset it later
        if (orbRenderer != null)
        {
            baseEmissionColor = orbRenderer.material.GetColor("_EmissionColor");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only react to hands (tagged as PlayerHand)
        if (other.CompareTag("PlayerHand"))
        {
            PlayNote();
        }
    }

    /// <summary>
    /// Called when triggered by raycast instead of touch
    /// </summary>
    public void PlayFromPointer()
    {
        PlayNote();
    }

    private void PlayNote()
    {
        // Play the sound
        if (note != null)
            note.Play();

        // Increase glow temporarily
        if (orbRenderer != null)
        {
            orbRenderer.material.SetColor("_EmissionColor", baseEmissionColor * 3f);
            Invoke(nameof(ResetGlow), 0.5f);
        }
    }

    private void ResetGlow()
    {
        if (orbRenderer != null)
            orbRenderer.material.SetColor("_EmissionColor", baseEmissionColor);
    }
}
