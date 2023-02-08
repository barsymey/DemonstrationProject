using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animation;
    int _velocityXHash;
    int _velocityZHash;
    int _lookHash;
    int isCarrying = 0;
    [SerializeField] private Transform _headTarget;
    [SerializeField] private Transform _leftHandTarget;
    [SerializeField] private Transform _rightHandTarget;

    void Awake()
    {
        _animation = GetComponent<Animator>();
        _velocityXHash = Animator.StringToHash("VelocityX");
        _velocityZHash = Animator.StringToHash("VelocityZ");
        _lookHash = Animator.StringToHash("Look");
    }

    void OnAnimatorIK(int layerIndex)
    {
        AimHeadToLook();
        AdjustHands();
    }

    public void ApplyMovement(float xVelocity, float zVelocity)
    {
        _animation.SetFloat(_velocityXHash, xVelocity);
        _animation.SetFloat(_velocityZHash, zVelocity);
    }

    private void AimHeadToLook()
    {
        _animation.SetLookAtWeight(1);
        _animation.SetLookAtPosition(_headTarget.position + _headTarget.forward);
    }

    private void AdjustHands()
    {
        _animation.SetIKPositionWeight(AvatarIKGoal.LeftHand, isCarrying);
        _animation.SetIKPositionWeight(AvatarIKGoal.RightHand, isCarrying);
        _animation.SetIKRotationWeight(AvatarIKGoal.LeftHand, isCarrying);
        _animation.SetIKRotationWeight(AvatarIKGoal.RightHand, isCarrying);
        _animation.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandTarget.position);
        _animation.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandTarget.rotation);
        _animation.SetIKPosition(AvatarIKGoal.RightHand, _rightHandTarget.position);
        _animation.SetIKRotation(AvatarIKGoal.RightHand, _rightHandTarget.rotation);
    }

    public void SetIsCarrying(bool state)
    {
        if(state)
            isCarrying = 1;
        else
            isCarrying = 0;
    }
}
