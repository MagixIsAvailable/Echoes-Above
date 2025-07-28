using UnityEngine;

public class OrbPointer : MonoBehaviour
{
    public LayerMask orbLayer; // Assign "Orb" layer
    public float maxDistance = 10f;

    void Update()
    {
        // Use right controller forward direction
        Transform hand = GameObject.Find("RightHandAnchor").transform;

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Ray ray = new Ray(hand.position, hand.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, orbLayer))
            {
                var orb = hit.collider.GetComponent<OrbInstrument>();
                if (orb != null)
                {
                    orb.PlayFromPointer();
                }
            }
        }
    }
}
