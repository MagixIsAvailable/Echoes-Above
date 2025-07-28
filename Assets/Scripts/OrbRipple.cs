using UnityEngine;

public class OrbRipple : MonoBehaviour
{
    public ParticleSystem rippleParticles;

    public void PlayRipple()
    {
        if (rippleParticles != null)
            rippleParticles.Play();
    }
}
