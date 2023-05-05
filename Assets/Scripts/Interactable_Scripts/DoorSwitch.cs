using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public Door Door_;
    private AudioSource au;
    private void Start()
    {
        au = GetComponent<AudioSource>();
    }
    public void Unlock()
    {
        au.Play();
        Door_.unlocked = true;
        Debug.Log(Door_.name + " unlocked values is equal to " + Door_.unlocked);
    }
}
