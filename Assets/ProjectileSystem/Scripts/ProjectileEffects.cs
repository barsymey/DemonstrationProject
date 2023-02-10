using UnityEngine;

/// <summary> A helper for ProjectileBehaviour to manage its lights and tracers </summary>
public class ProjectileEffects : MonoBehaviour
{
    public void InitEffects(bool state, float mass, Color color, bool isLighting, float lightScale)
    {
            gameObject.SetActive(state);
            if(!gameObject.activeInHierarchy)
                return;
            if(state)
            {
                float linearMultiplier = Mathf.Sqrt(mass);

                TrailRenderer trailRenderer = GetComponent<TrailRenderer>();
                trailRenderer.widthMultiplier = linearMultiplier /= 10;
                trailRenderer.endColor = color;
                trailRenderer.startColor = color;
                Material material = GetComponent<Renderer>().material;
                if(!isLighting)
                    material.DisableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color);

                Light light = GetComponentInChildren<Light>();
                light.gameObject.SetActive(isLighting);
                light.range = linearMultiplier * 10 * lightScale;
                light.color = color;
            }
    }
}
