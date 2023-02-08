using UnityEngine;

public class CarriageController : MonoBehaviour
{
    bool _isCarrying = false;
    public Transform carriedObject;
    private CarriableObject _carriableObject;

    public bool PickObject(Transform carriableObject)
    {
        if(carriableObject && !_isCarrying && carriableObject.TryGetComponent<CarriableObject>(out _carriableObject))
        {
            carriedObject = carriableObject;
            carriableObject.SetParent(transform);
            GetComponent<CarriableObject>().SetCarriedState(true);
            carriableObject.localPosition = new Vector3(0,0,0);
            carriableObject.localRotation = Quaternion.identity;
            _isCarrying = true;
            Debug.Log("picked");
        }
        else
        {
            if(carriedObject)
                DropObject();
        }
        return _isCarrying;
    }
    
    void DropObject()
    {
        GetComponent<CarriableObject>().SetCarriedState(false);
        carriedObject.SetParent(null);
        carriedObject = null;
        _isCarrying = false;
        Debug.Log("dropped");
    }
}
