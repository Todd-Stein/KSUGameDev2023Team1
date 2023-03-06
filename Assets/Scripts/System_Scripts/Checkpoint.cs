using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform[] nextGoals;
    public GameObject Tony;

    [SerializeField]
    GameObject nextDoor;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<MeshRenderer>().enabled = false;
        Tony = GameObject.Find("Tony");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            SavePlayerPosition(other.gameObject);
            SaveTonyPosition();

            if (nextGoals != null)
            {

                Tony.GetComponent<Tony>().goals = nextGoals;
                Tony.GetComponent<Tony>().goalIndex = 0;
                if (nextDoor != null)
                {
                    nextDoor.SetActive(true);
                    //gameObject.SetActive(false);
                }
            }

            gameObject.SetActive(false);
        }
    }

    void SavePlayerPosition(GameObject other)
    {
        PlayerPrefs.SetFloat("PlayerPosX", other.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", other.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", other.transform.position.z);
    }

    void SaveTonyPosition()
    {
        PlayerPrefs.SetFloat("TonyPosX", Tony.transform.position.x);
        PlayerPrefs.SetFloat("TonyPosY", Tony.transform.position.y);
        PlayerPrefs.SetFloat("TonyPosZ", Tony.transform.position.z);
    }
}