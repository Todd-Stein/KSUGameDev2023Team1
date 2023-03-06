using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPatrolSwitch : MonoBehaviour
{
    public Transform[] nextGoals;
    public GameObject Tony;

    [SerializeField]
    GameObject nextDoor;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        Tony = GameObject.Find("Tony");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && nextGoals != null)
        {
            Tony.GetComponent<Tony>().goals = nextGoals;
            Tony.GetComponent<Tony>().goalIndex = 0;
            if (nextDoor != null)
            {
                nextDoor.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}