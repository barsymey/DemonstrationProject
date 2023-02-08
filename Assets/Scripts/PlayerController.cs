using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float lookSensitivity = 3;

    private CharacterController _characterController;
    [SerializeField] Transform _raycastOrigin;
    private Transform _aimTarget;
    private Vector3 _aimPoint;
    [SerializeField] private float _interactionDistance = 10;
    

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetAim();
        HandleInput();
    }

    void HandleInput()
    {
        bool isForced = Input.GetKey(KeyCode.LeftShift);
        _characterController.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), isForced);
        _characterController.Turn(Input.GetAxis("Mouse X") * lookSensitivity);
        _characterController.Look(-Input.GetAxis("Mouse Y") * lookSensitivity);
        if(Input.GetKeyDown(KeyCode.Space))
            _characterController.Jump();
        if(Input.GetKeyDown(KeyCode.F))
            PickObject();
        if(Input.GetKeyDown(KeyCode.G))
            InteractTerrain();
    }

    void TargetAim()
    {
        RaycastHit raycastHit;
        if(Physics.Raycast(_raycastOrigin.position, _raycastOrigin.forward, out raycastHit, _interactionDistance))
        {
            _aimTarget = raycastHit.collider.transform;
            _aimPoint = raycastHit.point;
        }
        else
            _aimTarget = null;
        
        if(_aimTarget)
            InterfaceController.SetAimTargetName(_aimTarget.name);
        else
            InterfaceController.SetAimTargetName("");
    }

    void PickObject()
    {
        Debug.DrawRay(_raycastOrigin.position, _raycastOrigin.forward, Color.green, 2);
        _characterController.PickObject(_aimTarget);
    }

    void InteractTerrain()
    {
        TerrainController terrain;
        if(_aimTarget && _aimTarget.TryGetComponent<TerrainController>(out terrain))
        {
            terrain.AddTerrainHeight(_aimPoint);
        }
    }
}
