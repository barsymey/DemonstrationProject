using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float airResistance = 100;
    /// <summary> Speed im meter/second </summary>
    public float speed = 10;
    /// <summary> Mass in gramms </summary>
    public float mass = 1;
    public float lifetime = 3;
    public float spreadValue = 1;
    public bool persistsAfterStop = true;
    public bool traced = true;
    [Range (0, 1)]
    public float tracingProbability = 1;

    [SerializeField] GameObject tracer;
    [SerializeField] 

    private Vector3 _currentVelocity;
    RaycastHit _hit;
    Vector3 _nextPosition;
    private float _currentTime;
    private bool _isActive = true;
    

    void Start()
    {
        float scaleFactor = Mathf.Sqrt(mass);
        transform.localScale = Vector3.one * scaleFactor;
        if(traced)
        {
            float linearMultiplier = Mathf.Sqrt(mass/1000);
            tracer.SetActive(Random.Range(0,100) < tracingProbability * 100);
            tracer.GetComponent<TrailRenderer>().widthMultiplier = linearMultiplier;
            tracer.GetComponentInChildren<Light>().range = linearMultiplier * 10;
        }
        else
            tracer.SetActive(false);
        _currentVelocity = transform.forward * speed + GetRandomVector3(spreadValue);
        _currentTime = 0;
    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime > lifetime)
            Destroy(gameObject);
        if(_isActive)
            Advance();
    }

    void Advance()
    {
        _nextPosition = GetNextPosition();
        transform.rotation = Quaternion.LookRotation(_nextPosition - transform.position);
        if(Physics.Raycast(transform.position, transform.forward, out _hit, Vector3.Distance(transform.position, _nextPosition) * 3))
        {
            HitTarget();
            if(!_isActive)
                return;
        }
        else
            transform.position = _nextPosition;
        ApplyExternalForces();
    }

    void HitTarget()
    {
        Vector3 incoming = transform.forward;
        Vector3 reflected = Vector3.Reflect(incoming, _hit.normal);

        IProjectileTarget hitTarget;
        if(_hit.collider.TryGetComponent<IProjectileTarget>(out hitTarget))
            hitTarget.Impact(reflected, _hit, mass * _currentVelocity.magnitude);

        float angle = Vector3.Angle(incoming, reflected);

        Debug.DrawRay(_hit.point, reflected.normalized, Color.green, 1f);
        Debug.Log(angle);

        _currentVelocity -= _currentVelocity*((Mathf.Sin(Mathf.Deg2Rad * angle) + 1f)/2);
        transform.position = _hit.point;
        if(angle < 45 & _currentVelocity.magnitude > 5)
        {
            transform.rotation = Quaternion.LookRotation(reflected, transform.up);
            _currentVelocity = Vector3.RotateTowards(_currentVelocity, reflected, 1, 1) + GetRandomVector3(spreadValue/10);
        }
        else
        {
            if(!persistsAfterStop)
                Destroy(gameObject);
            transform.parent = _hit.collider.transform;
            _isActive = false;
        }
    }

    Vector3 GetNextPosition()
    {
        Vector3 result = transform.position + (_currentVelocity * Time.deltaTime);
        Debug.DrawLine(transform.position, result, Color.red, 0.1f);
        return result;
    }

    private Vector3 GetRandomVector3(float randomnessValue)
    {
        return new Vector3(
            Random.Range(0, randomnessValue),
            Random.Range(0, randomnessValue),
            Random.Range(0, randomnessValue)
        );
    }

    ///<summary>
    /// Applying air resistance and gravity
    ///</summary>
    private void ApplyExternalForces()
    {
        float massSqrt = Mathf.Sqrt(mass);
        _currentVelocity = new Vector3(
            _currentVelocity.x - _currentVelocity.x / (airResistance * massSqrt),
            _currentVelocity.y - _currentVelocity.y / (airResistance * massSqrt),
            _currentVelocity.z - _currentVelocity.z / (airResistance * massSqrt)
        ); 
        _currentVelocity += Physics.gravity * Time.deltaTime;
    }

}
