using UnityEngine;

public class SpringElement : MonoBehaviour
{
    Quaternion currentRotation;
    bool _isActing;

    void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.E))
            Punch(new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-30, 30)));
        HandleSpring();
    }

    void HandleSpring()
    {
        if(_isActing)
        {
            currentRotation = Quaternion.Slerp(currentRotation, Quaternion.identity, 0.03f);
            transform.localRotation = currentRotation;
            if(currentRotation.eulerAngles.magnitude < 1)
                _isActing = false;
        }
    }

    void Punch(Vector3 force)
    {
        _isActing = true;
        currentRotation = Quaternion.Euler(force);
    }
}
