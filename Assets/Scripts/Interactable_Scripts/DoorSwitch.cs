using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public Door Door_;

    public void Unlock()
    {
        Door_.unlocked = true;
    }
}
