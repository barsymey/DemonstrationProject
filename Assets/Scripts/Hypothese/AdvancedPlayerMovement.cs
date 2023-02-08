using UnityEngine;

public class AdvancedPlayerMovement : MonoBehaviour
{
    RaycastHit _graspTarget;
    public float speed = 10;
    float _currentRotation;
    bool _graspingMode;
    bool _isGrasping;
    Rigidbody _rigidbody;
    IUsableTool tool;
    [SerializeField] Transform toolTransform;
    // Start is called before the first frame update
    void Awake()
    {
        tool = toolTransform.GetComponent<IUsableTool>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }

    void Control()
    {
        if(CheckGrasp() && _graspingMode)
        {
            AttachToSurface();
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
        SetRigState(!_isGrasping);
        transform.Rotate(new Vector3(0, _currentRotation, 0), Space.Self);
        HandleInput();
    }

    void HandleInput()
    {
        Test();
        _currentRotation += Input.GetAxis("Mouse X")*Time.deltaTime * 100;
        if(Input.GetKeyDown(KeyCode.G))
            ToggleGrasp();
        if(Input.GetKeyDown(KeyCode.Space))
            UseTool();
        Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
        Move(movement);
    }

    void AttachToSurface()
    {
        if(_isGrasping)
        {            
            transform.position = _graspTarget.point;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, _graspTarget.normal);
            transform.Rotate(new Vector3(0, _currentRotation, 0), Space.Self);
        }
    }

    private bool CheckGrasp()
    {
        _isGrasping = Physics.Raycast(transform.position + (transform.up * 0.2f), -transform.up, out _graspTarget, 1f);
        Debug.DrawRay(transform.position + (transform.up * 0.2f), _graspTarget.normal, Color.black, 0.2f);
        Debug.DrawRay(transform.position + (transform.up * 0.2f), -transform.up, Color.red, 0.2f);
        return _isGrasping;
    }

    private void Move(Vector3 movement)
    {
        transform.Translate(movement * Time.deltaTime * speed, Space.Self);
    }

    void SetRigState(bool state)
    {
        _rigidbody.isKinematic = !state;
        _rigidbody.useGravity = state;
    }

    void ToggleGrasp()
    {
        _graspingMode = !_graspingMode;
    }

    void UseTool()
    {
        tool.Use();
    }

    void Test()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Find the line from the gun to the point that was clicked.
                Vector3 incomingVec = hit.point - transform.position;

                // Use the point's normal to calculate the reflection vector.
                Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);

                // Draw lines to show the incoming "beam" and the reflection.
                Debug.DrawLine(transform.position, hit.point, Color.red);
                Debug.DrawRay(hit.point, reflectVec, Color.green);
            }
        }

    }
}
