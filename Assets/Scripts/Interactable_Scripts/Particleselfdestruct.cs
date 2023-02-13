using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particleselfdestruct : MonoBehaviour
{

    private ParticleSystem emiter;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        emiter = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 1)
        {
            timer += Time.deltaTime;
        }
        if (emiter.particleCount == 0 && timer >= 1)
        {
            Object.Destroy(gameObject);
        }
    }
}
