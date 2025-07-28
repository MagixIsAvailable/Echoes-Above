using UnityEngine;

public class OrbPointer : MonoBehaviour
{
    public LayerMask orbLayer;
    public float maxDistance = 10f;

    void Update()
    {
        Debug.Log("OrbPointer Update running");

        // Use PointerPose (better alignment with laser)
        Transform rightHand = GameObject.Find("RightHandAnchor/PointerPose")?.transform;
        Transform leftHand = GameObject.Find("LeftHandAnchor/PointerPose")?.transform;

        // Draw rays so we can see them in Scene view
        if (rightHand != null)
            Debug.DrawRay(rightHand.position, rightHand.forward * maxDistance, Color.red);

        if (leftHand != null)
            Debug.DrawRay(leftHand.position, leftHand.forward * maxDistance, Color.blue);

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
        Debug.Log("Casting ray from: " + hand.name);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, orbLayer))
        {
            Debug.Log("Ray hit: " + hit.collider.name);

            OrbInstrument orb = hit.collider.GetComponent<OrbInstrument>();
            if (orb != null)
            {
                orb.PlayFromPointer();
            }
        }
    }
}
