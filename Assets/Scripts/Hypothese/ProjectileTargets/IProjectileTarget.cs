using UnityEngine;
/// <summary> 
/// An interface for interaction with projectiles.
///</summary>
public interface IProjectileTarget
{
    /// <summary> Impacts target and returns force absorbed by the impact </summary>
    public float Impact(Vector3 direction, RaycastHit hit, float force);
}

public struct ImpactInfo
{
    
}
