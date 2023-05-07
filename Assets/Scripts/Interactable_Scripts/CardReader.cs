using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReader : MonoBehaviour
{
    // array  of materials assigned in the inspector
    // materials are in Models/Reader/low-poly-eftpos-machine/source/Eftpos machiene/
    // array element 0 should have m_FullyLocked,  becasue 0 of 5 lights are green.
    // the 5th element should have m_Unlocked in it. because it has 5 lights on.
    [SerializeField]
    private Material[] materials;

    private MeshRenderer readerMesh;
    private Animation ani;

    private int keyCount = 0;

    [SerializeField]
    [Tooltip("This is the number of KeyCards that need to be used on this GameObject")]
    private int keysRequired = 5;

    [SerializeField]
    [Tooltip("Reference the door this CardReader unlocks.")]
    private Door door;

    private player_inventory playerInv;


    void Start()
    {
        readerMesh = GetComponent<MeshRenderer>();
        ani = GetComponent<Animation>();
        readerMesh.material = materials[keyCount];
        playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<player_inventory>();
    }



    public void Swipe()
    {
        // called from elsewhere
        // increment keycount
        // change material
        // if all keys used, unlock the door
        if (playerInv.Use())
        {
            keyCount++;
            StartCoroutine(ChangeMaterial());
            ani.Play();

            if (keyCount >= keysRequired)
                keyCount = keysRequired;

            if (keyCount == keysRequired)
                door.unlocked = true;
        }
    }

    private IEnumerator ChangeMaterial()
    {
        yield return new WaitForSeconds(1f);
        readerMesh.material = materials[keyCount];
    }
}
