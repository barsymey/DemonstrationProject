using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableObject : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _isCarried;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetCarriedState(bool state)
    {
        _isCarried = state;
        _rigidbody.isKinematic = !state;
        _rigidbody.detectCollisions = state;
    }

    public bool IsCarried()
    {
        return _isCarried;
    }
}
