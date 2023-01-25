using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tony : MonoBehaviour
{
    public bool hunting; // To be toggled when he is in hunt mode
    public bool alerted; // To be toggled when noise can/is heard by him
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        hunting = false;
        alerted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
