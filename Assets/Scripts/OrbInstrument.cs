using UnityEngine;

public class OrbInstrument : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public AudioSource note;          // The AudioSource on this orb
    public Renderer orbRenderer;      // Mesh Renderer of the orb (for glow)

    private Color baseEmissionColor;
    private FloatingSwarmOrb[] cachedSwarmOrbs;

    void Start()
    {
        if (orbRenderer != null)
        {
            baseEmissionColor = orbRenderer.material.GetColor("_EmissionColor");
        }

        // Cache all swarm orbs in the scene (so we don’t repeatedly search)
        cachedSwarmOrbs = FindObjectsOfType<FloatingSwarmOrb>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            PlayNote();
        }
    }

    public void PlayFromPointer()
    {
        PlayNote();
    }

    private void PlayNote()
    {
        // Play sound without cutting off previous sounds
        if (note != null && note.clip != null)
            note.PlayOneShot(note.clip);

        // Glow effect
        if (orbRenderer != null)
        {
            orbRenderer.material.SetColor("_EmissionColor", baseEmissionColor * 3f);
            Invoke(nameof(ResetGlow), 0.5f);
        }

        // Change background color
        BackgroundColorController bg = FindObjectOfType<BackgroundColorController>();
        if (bg != null)
        {
            Color orbColor = orbRenderer.material.GetColor("_EmissionColor");
            bg.SetNewBackgroundColor(orbColor);
        }

        // Trigger cached swarm orb reactions
        if (cachedSwarmOrbs != null)
        {
            Color orbColor = orbRenderer.material.GetColor("_EmissionColor");
            foreach (var so in cachedSwarmOrbs)
            {
                if (so != null)
                {
                    so.ChangeColor(orbColor);
                    so.PlayEcho(note.clip);
                }
            }
        }
    }

    private void ResetGlow()
    {
        if (orbRenderer != null)
            orbRenderer.material.SetColor("_EmissionColor", baseEmissionColor);
    }
}
