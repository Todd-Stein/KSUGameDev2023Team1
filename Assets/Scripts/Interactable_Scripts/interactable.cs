using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{


    public Door door = null;
    public DoorSwitch DS = null;
    public CardReader cardReader = null;  

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

        if (cardReader != null)
            Debug.Log (cardReader.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        if(door != null)
        {
            door.open();
            return;
        }
        if(DS != null)
        {
            Debug.Log("UNLOCKING DOOR");
            //unlock called
            DS.Unlock();
            return;
        }

        if (cardReader != null)
        {
            Debug.Log("swiping card");
            cardReader.Swipe();
            return;
        }

        gameObject.GetComponent<SoundEvent>().activateNoiseEvent();
        Debug.Log("Interactable Activated");
    }
}
