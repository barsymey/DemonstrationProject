using UnityEngine;

public class SolidProjectileTarget : MonoBehaviour, IProjectileTarget
{
    [SerializeField] Transform solidParticles;
    [SerializeField] Transform dustParticles;
    public float Impact(Vector3 direction, RaycastHit hit, float force)
    {
        force /= 100000;
        force = Mathf.Sqrt(force);
        if( force > 0.3)
        {
            if(dustParticles)
            {
                Transform obj = Instantiate(dustParticles, hit.point, Quaternion.LookRotation(hit.normal));
                obj.localScale = Vector3.one * force;
            }
            if(solidParticles)
            {
                Transform obj = Instantiate(solidParticles, hit.point, Quaternion.LookRotation(direction));
                obj.localScale = Vector3.one * force;
            }
        }
        return 1;
    }
}
