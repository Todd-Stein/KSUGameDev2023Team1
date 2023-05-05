using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractItem : MonoBehaviour
{
    public bool thrown;
    private SoundEvent Sevent;
    private Collider cl;
    public GameObject emiter;
    public bool collide = false;

    // Start is called before the first frame update
    void Start()
    {
        thrown = false;
        Sevent = gameObject.GetComponentInChildren<SoundEvent>();
        cl = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
       if(collide && !Sevent.GetComponent<AudioSource>().isPlaying)
        {
            Object.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(thrown == true)
        {
            Debug.Log("Entered Collission");
            Sevent.activateNoiseEvent();
            Instantiate(emiter, gameObject.transform.position, Quaternion.identity);
            //Object.Destroy(gameObject);
            try { gameObject.GetComponent<Collider>().enabled = false; }
            catch { }
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            collide = true;
            thrown = false;
        }
    }
}
