using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{


    public Door door = null;
    public DoorSwitch DS = null;
    // Start is called before the first frame update
    void Start()
    {
        if (door != null)
        {
            Debug.Log(door.name);
        }
        if(DS != null)
        {
            Debug.Log (DS.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] coliders = Physics.OverlapSphere(gameObject.GetComponent<CapsuleCollider>().center, 5.0f, 8);
        if(coliders.Length != 0)
        {
            foreach(var T in coliders)
            {
                if(T.GetComponent<Tony>().idleTimer != 0)
                {
                    gameObject.GetComponent<SoundEvent>().stopPlaying();
                }
            }
        }

    }

    public void activate()
    {
        if(door != null)
        {
            door.open();
            return;
        }
        if(DS != null)
        {
            //unlock called
            DS.Unlock();
            return;
        }
        gameObject.GetComponent<SoundEvent>().activateNoiseEvent();
        Debug.Log("Interactable Activated");
    }
}
