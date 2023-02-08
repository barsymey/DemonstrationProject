using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraHeight;
    public float cameraDistance;
    [SerializeField] private Transform _cameraHeightOffsetPosition;
    [SerializeField] private Transform _cameraDistanceOffsetPosition;
    private float _cmaeraAngle;

    public void AdjustCamera(float value)
    {
        _cmaeraAngle += value;
        _cmaeraAngle = Mathf.Clamp(_cmaeraAngle, -90, 90);
        transform.localRotation = Quaternion.Euler(_cmaeraAngle, 0, 0);
        _cameraHeightOffsetPosition.localPosition = new Vector3(0, cameraHeight, 0);
        _cameraDistanceOffsetPosition.localPosition = new Vector3(0, 0, cameraDistance);
    }
}
