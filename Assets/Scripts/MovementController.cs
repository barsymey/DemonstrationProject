using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float maxRunSpeed = 20;
    public float maxWalkSpeed = 10;
    public float  moveForce = 10000;
    public float jumpForce = 10000;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public Vector3 Move(float xValue, float zValue, bool forced)
    {
        float maxSpeed = maxWalkSpeed;
        if(forced)
            maxSpeed = maxRunSpeed;
        if(_rigidbody.velocity.magnitude < maxSpeed && IsGrounded())
            _rigidbody.AddRelativeForce(new Vector3(xValue, 0 ,zValue).normalized * Time.deltaTime * moveForce, ForceMode.Force);
        return transform.InverseTransformVector(_rigidbody.velocity);
    }

    public void Jump()
    {
        if (IsGrounded())
            _rigidbody.AddForce(transform.up * jumpForce);
    }

    private bool IsGrounded()
    {
        Vector3 origin = transform.position + new Vector3(0, 0.1f, 0);
        Vector3 target = -transform.up;
        RaycastHit hit;
        return Physics.Raycast(origin, target, out hit, 0.4f);
    }
}
