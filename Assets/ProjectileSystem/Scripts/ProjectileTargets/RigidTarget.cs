using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidTarget : MonoBehaviour, IProjectileTarget
{
    Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public float Impact(Vector3 direction, RaycastHit hit, float force)
    {
        _rigidbody.AddForceAtPosition(direction*1000, hit.point, ForceMode.Force);
        return 1;
    }
}
