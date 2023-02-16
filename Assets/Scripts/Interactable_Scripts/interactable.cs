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
    }

    // Update is called once per frame
    void Update()
    {
        
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
