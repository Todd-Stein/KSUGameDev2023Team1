using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public Door Door_;
    private SphereCollider area;

    // Start is called before the first frame update
    void Start()
    {
        area = gameObject.GetComponent<SphereCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player Enter");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Door_.unlocked = true;
            }
        }
    }
}
