using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Tooltip("The minimum time between flickers")]
    public float minFlickerTime = 0.2f;

    [Tooltip("The maximum time between flickers")]
    public float maxFlickerTime = 0.5f;

    [Tooltip("The intensity the light should flicker to when it flickers")]
    public float flickerIntensity = 0.5f;

    [Tooltip("The materials to flicker between when a light is flickered")]
    public Material[] flickerMaterials;

    private List<Light> lights;
    private List<float> lightIntensities;

    private float nextFlickerTime;
    MeshRenderer parentRenderer;

    void Start()
    {
        // Get all the lights in the scene
        lights = new List<Light>(FindObjectsOfType<Light>());

        // Save the initial intensities of the lights
        lightIntensities = new List<float>();
        foreach (Light light in lights)
        {
            lightIntensities.Add(light.intensity);
        }

        // Set the time for the next flicker
        nextFlickerTime = Time.time + Random.Range(minFlickerTime, maxFlickerTime);
    }

    void Update()
    {
        if (Time.time >= nextFlickerTime)
        {
            //choose a random light to flicker
            int randomIndex = Random.Range(0, lights.Count);
            Light randomLight = lights[randomIndex];
            parentRenderer = randomLight.transform.parent.GetComponent<MeshRenderer>();

            //mat change
            parentRenderer.material = flickerMaterials[1];
            //flicker
            randomLight.intensity = flickerIntensity;         

            nextFlickerTime = Time.time + Random.Range(minFlickerTime, maxFlickerTime);
        }

        else
        {
            if (parentRenderer != null)
                parentRenderer.material = flickerMaterials[0];
            //reset lights to their initial intensity
            for (int i = 0; i < lights.Count; i++)
            {
                Light light = lights[i];
                light.intensity = lightIntensities[i];
            }
        }
    }


}
