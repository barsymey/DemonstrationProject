using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private MovementController _movementController;
    private AnimationController _animationController;
    private CameraController _cameraController;
    private CarriageController _carriageController;

    void Awake()
    {
        _movementController = GetComponent<MovementController>();
        _animationController = GetComponentInChildren<AnimationController>();
        _cameraController = GetComponentInChildren<CameraController>();
        _carriageController = GetComponentInChildren<CarriageController>();
    }

    public void Move(float xValue, float zValue, bool forced)
    {
        Vector3 relativeAcceleration = _movementController.Move(xValue, zValue, forced);
        _animationController.ApplyMovement(relativeAcceleration.x, relativeAcceleration.z);
    }

    public void Turn(float value)
    {
        transform.Rotate(new Vector3(0, value, 0), Space.World);
    }

    public void Look(float value)
    {
        _cameraController.AdjustCamera(value);
    }

    public void Jump()
    {
        _movementController.Jump();
    }

    public void PickObject(Transform pickedObject)
    {
        _animationController.SetIsCarrying(_carriageController.PickObject(pickedObject));
    }

}
