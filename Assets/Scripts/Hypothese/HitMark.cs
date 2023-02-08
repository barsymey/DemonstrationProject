using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMark : MonoBehaviour
{
    float _timeCreated;
    float _lifetime = 1;
    // Start is called before the first frame update
    void Start()
    {
        _timeCreated = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > _timeCreated + _lifetime)
            Destroy(gameObject);
    }
}
