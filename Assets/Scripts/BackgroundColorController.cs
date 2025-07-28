using UnityEngine;
using System.Collections;

public class BackgroundColorController : MonoBehaviour
{
    [System.Serializable]
    public struct GradientSet
    {
        public Color top;
        public Color middle;
        public Color bottom;
    }

    public Camera mainCamera;

    // Presets (auto-filled in Start)
    public GradientSet yellowOrange;
    public GradientSet blueTurquoise;
    public GradientSet violet;
    public GradientSet green;

    // Current gradient values
    private Color currentTop;
    private Color currentMid;
    private Color currentBot;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        // --- Fill in presets automatically using hex values ---
        ColorUtility.TryParseHtmlString("#452C73", out yellowOrange.top);
        ColorUtility.TryParseHtmlString("#D96B29", out yellowOrange.middle);
        ColorUtility.TryParseHtmlString("#FFA94D", out yellowOrange.bottom);

        ColorUtility.TryParseHtmlString("#1F3F72", out blueTurquoise.top);
        ColorUtility.TryParseHtmlString("#4FB4D9", out blueTurquoise.middle);
        ColorUtility.TryParseHtmlString("#D3F1FF", out blueTurquoise.bottom);

        ColorUtility.TryParseHtmlString("#3A246C", out violet.top);
        ColorUtility.TryParseHtmlString("#A784D9", out violet.middle);
        ColorUtility.TryParseHtmlString("#E9D3FF", out violet.bottom);

        ColorUtility.TryParseHtmlString("#0F3E3D", out green.top);
        ColorUtility.TryParseHtmlString("#42A887", out green.middle);
        ColorUtility.TryParseHtmlString("#B8F5D3", out green.bottom);

        // Initialize with a neutral color
        currentTop = new Color(0.1f, 0.1f, 0.2f);
        currentMid = new Color(0.1f, 0.1f, 0.25f);
        currentBot = new Color(0.2f, 0.2f, 0.3f);
    }

    void Update()
    {
        // For simplicity, use middle gradient color for the camera background.
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = currentMid;
        }
    }

    /// <summary>
    /// Smoothly transitions to a new gradient set over time.
    /// </summary>
    public void SetGradient(GradientSet newGradient)
    {
        StopAllCoroutines();
        StartCoroutine(LerpGradient(newGradient, 2f)); // 2-second transition
    }

    private IEnumerator LerpGradient(GradientSet target, float duration)
    {
        Color startTop = currentTop;
        Color startMid = currentMid;
        Color startBot = currentBot;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            currentTop = Color.Lerp(startTop, target.top, t);
            currentMid = Color.Lerp(startMid, target.middle, t);
            currentBot = Color.Lerp(startBot, target.bottom, t);

            yield return null;
        }

        currentTop = target.top;
        currentMid = target.middle;
        currentBot = target.bottom;
    }
}
