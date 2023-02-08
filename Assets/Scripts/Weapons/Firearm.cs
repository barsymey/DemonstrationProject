using UnityEngine;

public class Firearm : MonoBehaviour, IUsableTool
{
    [SerializeField] Transform barrelEnd;
    [SerializeField] Transform projectile;
    [SerializeField] uint projectileAmount;
    [SerializeField] ParticleSystem shotFlash;

    public void Use()
    {
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
    }
}
