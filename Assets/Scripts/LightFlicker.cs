using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private Light2D light;
    private int frames = 0;
    [SerializeField] private int framesPerRandomize;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;

    void Start()
    {

    }

    void Update()
    {
        frames++;
        if (frames % framesPerRandomize == 0)
        {
            RandomizeIntensity();
        }
    }

    void RandomizeIntensity()
    {
        float randomValue = UnityEngine.Random.Range(minValue, maxValue);
        light.intensity = randomValue;
    }
}
