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
            // Choose gradient based on orb color or index
            Color orbColor = orbRenderer.material.GetColor("_EmissionColor");
            BackgroundColorController.GradientSet selected = bg.blueTurquoise;

            // Simple mapping (you can refine this)
            if (orbColor.r > 0.9f && orbColor.g > 0.7f) selected = bg.yellowOrange; // Yellow/Orange
            else if (orbColor.b > 0.8f && orbColor.g > 0.7f) selected = bg.blueTurquoise; // Blues
            else if (orbColor.b > 0.8f && orbColor.r > 0.6f) selected = bg.violet; // Violet
            else if (orbColor.g > 0.7f && orbColor.r < 0.6f) selected = bg.green; // Green

            bg.SetGradient(selected);
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
