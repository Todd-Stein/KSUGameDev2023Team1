using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractItem : MonoBehaviour
{
    public bool thrown;
    private SoundEvent Sevent;
    private Collider cl;
    public GameObject emiter;

    // Start is called before the first frame update
    void Start()
    {
        thrown = false;
        Sevent = gameObject.GetComponent<SoundEvent>();
        cl = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(thrown == true)
        {

            Sevent.activateNoiseEvent();
            Instantiate(emiter, gameObject.transform.position, Quaternion.identity);
            Object.Destroy(gameObject);
        }
    }
}
