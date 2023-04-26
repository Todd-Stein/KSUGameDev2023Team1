using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    public bool unlocked = false;
    public bool opened = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Debug
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Door unlocked: " + unlocked.ToString());
        }

    }

    public void open()
    {
        if (unlocked)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            opened = true;
            GetComponent<NavMeshObstacle>().enabled = false;
            //gameObject.GetComponent<SoundEvent>().activateNoiseEvent();
            
        }
    }

    public void ForceOpen()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SoundEvent>().activateNoiseEvent();
    }
}
