using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform[] nextGoals;
    public GameObject Tony;

    [SerializeField]
    GameObject nextDoor;

    static private bool used;

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
        if (other.CompareTag("Player") && !used)
        {
            used = true;
            SavePlayerState(other.gameObject);
            SaveTonyState();

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

            //gameObject.SetActive(false);
        }
    }

    void SavePlayerState(GameObject other)
    {
        PlayerPrefs.SetFloat("PlayerPosX", other.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", other.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", other.transform.position.z);
    }

    void SaveTonyState()
    { 
        PlayerPrefs.SetFloat("TonyPosX", Tony.transform.position.x);
        PlayerPrefs.SetFloat("TonyPosY", Tony.transform.position.y);
        PlayerPrefs.SetFloat("TonyPosZ", Tony.transform.position.z);
        PlayerPrefs.SetInt("TonyAggression", Tony.GetComponent<Tony>().aggression);
    }
}