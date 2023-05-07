using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardScript : MonoBehaviour
{
    private Animation ani;


    void Start()
    {
        ani = GetComponent<Animation>();
    }

    public void GetPickedUp()
    {
        ani.Play();
    }
}
