using UnityEngine;

public class OrbPointer : MonoBehaviour
{
    public LayerMask orbLayer;
    public float maxDistance = 10f;
    public Material laserMaterial; // Assign in inspector
    public float laserWidth = 0.01f;

    private Transform rightHand;
    private Transform leftHand;
    private LineRenderer rightLaser;
    private LineRenderer leftLaser;

    void Start()
    {
        // Cache hand transforms
        rightHand = GameObject.Find("RightControllerAnchor")?.transform;
        leftHand = GameObject.Find("LeftControllerAnchor")?.transform;


        // Create laser line renderers
        if (rightHand != null)
        {
            rightLaser = CreateLaser(rightHand.gameObject, Color.red);
        }

        if (leftHand != null)
        {
            leftLaser = CreateLaser(leftHand.gameObject, Color.blue);
        }
    }

    LineRenderer CreateLaser(GameObject parent, Color color)
    {
        GameObject laserObj = new GameObject("Laser");
        laserObj.transform.SetParent(parent.transform);

        LineRenderer laser = laserObj.AddComponent<LineRenderer>();

        // Create a simple material if none is assigned
        if (laserMaterial == null)
        {
            laserMaterial = new Material(Shader.Find("Sprites/Default"));
            laserMaterial.color = color;
        }

        laser.material = laserMaterial;
        laser.startColor = color;
        laser.endColor = color;
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;
        laser.positionCount = 2;
        laser.useWorldSpace = true;
        laser.sortingOrder = 1000; // Make sure it renders on top

        return laser;
    }

    void Update()
    {
        // Update laser visuals
        UpdateLaser(rightHand, rightLaser);
        UpdateLaser(leftHand, leftLaser);

        // Debug: Check if hands are found
        if (rightHand == null)
            Debug.LogWarning("Right hand not found!");
        if (leftHand == null)
            Debug.LogWarning("Left hand not found!");

        // Right-hand trigger
        if (rightHand != null && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("Right trigger pressed!");
            CastRayFromHand(rightHand);
        }

        // Left-hand trigger
        if (leftHand != null && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("Left trigger pressed!");
            CastRayFromHand(leftHand);
        }
    }

    void UpdateLaser(Transform hand, LineRenderer laser)
    {
        if (hand == null || laser == null) return;

        Vector3 startPos = hand.position;
        Vector3 endPos = startPos + hand.forward * maxDistance;

        // Check if laser hits something to adjust end position (check ALL layers first)
        if (Physics.Raycast(hand.position, hand.forward, out RaycastHit hit, maxDistance))
        {
            endPos = hit.point;

            // Debug what we're hitting
            if ((orbLayer.value & (1 << hit.collider.gameObject.layer)) != 0)
            {
                Debug.Log($"Laser pointing at orb: {hit.collider.name}");
            }
        }

        laser.SetPosition(0, startPos);
        laser.SetPosition(1, endPos);
        laser.enabled = true; // Make sure laser is enabled
    }

    void CastRayFromHand(Transform hand)
    {
        Ray ray = new Ray(hand.position, hand.forward);
        Debug.Log("Casting ray from: " + hand.name + " at position: " + hand.position);

        // First check if we hit anything at all
        if (Physics.Raycast(ray, out RaycastHit anyHit, maxDistance))
        {
            Debug.Log($"Ray hit something: {anyHit.collider.name} on layer: {anyHit.collider.gameObject.layer}");
        }

        // Then check specifically for orb layer
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, orbLayer))
        {
            Debug.Log("Ray hit orb: " + hit.collider.name);

            OrbInstrument orb = hit.collider.GetComponent<OrbInstrument>();
            if (orb != null)
            {
                Debug.Log("Playing orb sound!");
                orb.PlayFromPointer();
            }
            else
            {
                Debug.LogWarning("Hit object has no OrbInstrument component!");
            }
        }
        else
        {
            Debug.Log("No orb hit by raycast. Check layer mask settings.");
        }
    }
}
