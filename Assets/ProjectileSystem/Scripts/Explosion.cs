using UnityEngine;

/// <summary> Only spawns frags and visuals for now </summary>
public class Explosion : MonoBehaviour
{
    public uint fragmentAmount = 0;
    public float strength = 1;
    [SerializeField] Transform fragmentPrefab;
    [SerializeField] Transform visualEffect;

    void Start()
    {
        Explode();
    }

    void Explode()
    {
        if(fragmentPrefab && fragmentAmount > 0)
            CreateFrags();
        Instantiate(visualEffect, transform.position, transform.rotation);
        Shockwave();
        Destroy(gameObject);
    }

    void CreateFrags()
    {
        for(int i = 0; i < fragmentAmount; i++)
            {
                Instantiate(fragmentPrefab, transform.position, Quaternion.Euler(new Vector3(
                    Random.Range(0, 360),
                    Random.Range(0, 360),
                    Random.Range(0, 360)
                )));
            }
    }

    void Shockwave()
    {
        
    }
}
