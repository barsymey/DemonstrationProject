using UnityEngine;

/// <summary> Projectile behaviour that is not based upon default rigidbody system</summary>
public class ProjectileBehaviour : MonoBehaviour
{
    /// <summary> initialSpeed im meter/second </summary>
    public float initialSpeed = 10;
    /// <summary> Mass in gramms. Influences impact and drag force </summary>
    public float mass = 1;
    /// <summary> The more - the less drag from air. Means that there will be no significant drag below this velocity </summary>
    public float aerodynamicCoefficient = 100;
    /// <summary> The more - the more drag from air. Didnt want to mess with density </summary>
    public float aerodynamicResistance = 1;
    public float lifetime = 3;
    public float spreadAngle = 1;
    public float ricochetAngle = 45;
    public float ricochetVelocity = 1;
    public bool persistsAfterStop = true;

    [Header ("Explosion settings")]
    public bool explosive = false;
    [SerializeField] Transform explosionPrefab;
    public float explosionTriggerVelocity = 20;
    public float explosionTriggerAngle = 20;
    public bool explodesOnTimer= false;

    [Header ("Tracer settings")]
    public bool traced = true;
    [SerializeField] ProjectileEffects effects;
    [Range (0, 1)]
    public float tracingProbability = 1;
    public Color tracerColor = Color.yellow;
    public bool tracerLight = true;
    [Range (0, 10)]
    public float lightSclae = 1;

    private Vector3 _currentVelocity;
    RaycastHit _hit;
    Vector3 _nextPosition;
    private float _currentTime;
    private bool _isActive = true;

    void Start()
    {
        InitProjectile();
    }

    void LateUpdate()
    {
        UpdateProjectile();
    }

    private void InitProjectile()
    {
        float scaleFactor = Mathf.Sqrt(mass);
        transform.localScale = Vector3.one * scaleFactor;

        if(effects)
            effects.InitEffects(traced & Random.Range(0,100) < tracingProbability * 100, mass, tracerColor, tracerLight, lightSclae);

        _currentVelocity = Quaternion.Euler(GetRandomVector3(spreadAngle)) * (transform.forward * initialSpeed);
        _currentTime = 0;
    }

    private void UpdateProjectile()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime > lifetime)
        {
            if(explodesOnTimer)
                Explode();
            Destroy(gameObject);
            return;
        }
        if(_isActive)
            Advance();
        else
            if(!persistsAfterStop)
                Explode();
        ApplyExternalForces();
    }

    private void Advance()
    {
        _nextPosition = GetNextPosition();
        // Facing to next waypoint
        transform.rotation = Quaternion.LookRotation(_nextPosition - transform.position);
        if(Physics.Raycast(transform.position, transform.forward, out _hit, Vector3.Distance(transform.position, _nextPosition)))
        {
            HitTarget();
        }
        else
            transform.position = _nextPosition;
    }

    private void HitTarget()
    {
        Vector3 incoming = transform.forward;
        Vector3 reflected = Vector3.Reflect(incoming, _hit.normal);
        float angle = Vector3.Angle(incoming, reflected);

        float impactPercentage = (Mathf.Sin(Mathf.Deg2Rad * angle) + 1f)/2;

        // Handling special target behaviour
        IProjectileTarget hitTarget;
        if(_hit.collider.TryGetComponent<IProjectileTarget>(out hitTarget))
            hitTarget.Impact(reflected, _hit, mass * _currentVelocity.magnitude * impactPercentage);

        // Projectile slowing down depends on impact angle
        _currentVelocity -= _currentVelocity * impactPercentage;

        transform.position = _hit.point;
        _nextPosition = transform.position;

        // Checking explosion conditions
        if(explosive && angle > explosionTriggerAngle && _currentVelocity.magnitude > explosionTriggerVelocity)
        {
            Explode();
            return;
        }

        if(angle < ricochetAngle && _currentVelocity.magnitude > ricochetVelocity)
        {
            transform.rotation = Quaternion.LookRotation(reflected, transform.up);
            _currentVelocity = Vector3.RotateTowards(_currentVelocity, reflected, 1, 1);
        }
        else
        // The flight is over
        {
            transform.parent = _hit.collider.transform;
            _isActive = false;
        }
    }

    private Vector3 GetNextPosition()
    {
        Vector3 result = transform.position + (_currentVelocity * Time.deltaTime);
        Debug.DrawLine(transform.position, result, Color.red, 0.1f);
        return result;
    }

    ///<summary>
    /// Applying air resistance and gravity by simplified formula
    ///</summary>
    private void ApplyExternalForces()
    {
        _currentVelocity -= Time.deltaTime * (_currentVelocity /aerodynamicCoefficient) * (aerodynamicResistance / mass);
        _currentVelocity += Physics.gravity * 3 * Time.deltaTime;
    }

    // Is public in case you want to explode it remotely
    public void Explode()
    {
        if(explosionPrefab & explosive)
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private Vector3 GetRandomVector3(float randomnessValue)
    {
        return new Vector3(
            Random.Range(-randomnessValue, randomnessValue),
            Random.Range(-randomnessValue, randomnessValue),
            Random.Range(-randomnessValue, randomnessValue)
        );
    }

}
