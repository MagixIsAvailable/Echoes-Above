using UnityEngine;

public class OrbPointer : MonoBehaviour
{
    public LayerMask orbLayer;
    public float maxDistance = 10f;

    void Update()
    {
        // Get both hand anchors
        Transform rightHand = GameObject.Find("RightHandAnchor")?.transform;
        Transform leftHand = GameObject.Find("LeftHandAnchor")?.transform;

        // Right-hand trigger
        if (rightHand != null && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            CastRayFromHand(rightHand);
        }

        // Left-hand trigger
        if (leftHand != null && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            CastRayFromHand(leftHand);
        }
    }

    void CastRayFromHand(Transform hand)
    {
        Ray ray = new Ray(hand.position, hand.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, orbLayer))
        {
            OrbInstrument orb = hit.collider.GetComponent<OrbInstrument>();
            if (orb != null)
            {
                orb.PlayFromPointer();
            }
        }
    }
}
