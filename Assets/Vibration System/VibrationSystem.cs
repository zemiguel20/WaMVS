using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// ONLY ONE INSTANCE SHOULD EXIST
public class VibrationSystem : MonoBehaviour
{
    [SerializeField] private ActionBasedController left;
    [SerializeField] private ActionBasedController right;
    [SerializeField] private Camera head;
    
    // Distance-Intensity curve for the vibration
    [SerializeField] private AnimationCurve vibrationIntensityCurve;
    // Relative angle - Amplitude curves for stereo. 0-0.5 Right, 0.5-1 Left 
    [SerializeField] private AnimationCurve rightStereoAmpCurve;
    [SerializeField] private AnimationCurve leftStereoAmpCurve;
    [SerializeField] private float vibrationDuration;

    // Spatial vibration
    // Location mapping from top-down 2D view.
    public void PlayVibration(Transform source)
    {
        // Get top-down vectors
        Vector3 topDownSourcePos = source.position;
        topDownSourcePos.y = 0;
        Vector3 topDownHeadPos = head.transform.position;
        topDownHeadPos.y = 0;
        Vector3 topDownForward = head.transform.forward;
        topDownForward.y = 0;

        Vector3 sourceRelativePosition = topDownSourcePos - topDownHeadPos;

        // Angle relative to player orientation
        float angle = Vector3.SignedAngle(sourceRelativePosition, topDownForward, Vector3.up);
        // +1 = Full Left, -1 = Full Right
        float stereoProportion= Mathf.Sin(angle * Mathf.Deg2Rad);
        // Normalize
        stereoProportion = (stereoProportion + 1.0f) * 0.5f;
        // Get stereo weights
        float stereoLeftWeight = leftStereoAmpCurve.Evaluate(stereoProportion);
        float stereoRightWeight = rightStereoAmpCurve.Evaluate(stereoProportion);

        // Calculate base volume through distance
        float intensity = vibrationIntensityCurve.Evaluate(sourceRelativePosition.magnitude);

        left.SendHapticImpulse(intensity * stereoLeftWeight, vibrationDuration);
        right.SendHapticImpulse(intensity * stereoRightWeight, vibrationDuration);
    }
}
