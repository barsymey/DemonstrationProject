using UnityEngine;
using System.Collections.Generic;

public class AdvancedPlayerMovement : MonoBehaviour
{
    RaycastHit _graspTarget;
    public float speed = 10;
    float _currentRotation;
    bool _graspingMode;
    bool _isGrasping;
    Rigidbody _rigidbody;
    List<IUsableTool> tools;
    [SerializeField] Transform toolPivotTransform;
    // Start is called before the first frame update
    void Awake()
    {
        tools = new List<IUsableTool>();
        foreach(IUsableTool tool in GetComponentsInChildren<IUsableTool>())
            tools.Add(tool);
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
        _currentRotation += Input.GetAxis("Mouse X")*Time.deltaTime * 100;
        toolPivotTransform.Rotate(new Vector3(-Input.GetAxis("Mouse Y")*Time.deltaTime * 100, 0, 0));
        if(Input.GetKeyDown(KeyCode.G))
            ToggleGrasp();
        if(Input.GetKey(KeyCode.Space))
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
        foreach(IUsableTool tool in tools)
            tool.Use();
    }
}
