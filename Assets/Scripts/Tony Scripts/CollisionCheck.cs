using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    Tony tony;

    private void Start()
    {
        tony = GetComponentInParent<Tony>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SoundEvent>())
        {
            Debug.Log("NoizeMaker found");
        }
    }
}
