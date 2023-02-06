using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controls : MonoBehaviour
{
    public KeyCode enableGoggles = KeyCode.Mouse1;
    public KeyCode pickup = KeyCode.E;
    public KeyCode toss = KeyCode.Mouse0;

    [SerializeField]
    private GameObject tonyVision;

    //throwing control




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(enableGoggles))
        {
            tonyVision.SetActive(true);
           
        }
        if (Input.GetKeyUp(enableGoggles))
        {
            tonyVision.SetActive(false);
        }
    }
}
