using UnityEngine;

public class Firearm : MonoBehaviour, IUsableTool
{
    [SerializeField] float roundDelay = 0.5f;
    [SerializeField] Transform barrelEnd;
    [SerializeField] Transform projectile;
    [SerializeField] uint projectileAmount;
    [SerializeField] ParticleSystem shotFlash;

    private float _lastShotTime;

    public void Use()
    {
        if(_lastShotTime < Time.time)
            Shoot();
    }

    private void Shoot()
    {
        if(shotFlash)
        {
            shotFlash.gameObject.SetActive(true);
            shotFlash.Play(true);
        }
        for(int i = 0 ; i < projectileAmount; i++)
        {
            Instantiate(projectile, barrelEnd.position, barrelEnd.rotation);
        }
        _lastShotTime = Time.time + roundDelay;
    }
}
