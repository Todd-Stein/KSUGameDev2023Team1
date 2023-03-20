using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Checkpoint : MonoBehaviour
{
    public Transform[] nextGoals;
    public GameObject Tony;
    public GameObject player;
    GameObject held;

    [SerializeField]
    GameObject nextDoor;
    [SerializeField]
    GameObject previousDoor;

    //[SerializeField]
    static private bool used;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<MeshRenderer>().enabled = false;
        Tony = GameObject.Find("Tony");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !used)
        {
            Debug.Log("Passed Checkpoint.");
            used = true;
            SavePlayerState(other.gameObject);
            if (Tony != null) { SaveTonyState(); }

            if (nextGoals != null && Tony != null)
            {
                Tony.GetComponent<Tony>().goals = nextGoals;
                Tony.GetComponent<Tony>().goalIndex = 0;
            }

            if (nextDoor != null)
            {
                nextDoor.SetActive(true);
                nextDoor.GetComponent<Checkpoint>().SetPrevDoor(gameObject);
                //gameObject.SetActive(false);
            }

            if (previousDoor != null)
            {
                previousDoor.SetActive(false);
            }
            // Move to SavePlayerState()
            /*if (other.GetComponent<player_controls>().GetHolding())
            {
                held = other.GetComponent<player_controls>().GetItem();
            } else
            {
                held = null;
            }*/
        }
    }

    void SavePlayerState(GameObject other)
    {
        PlayerPrefs.SetFloat("PlayerPosX", other.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", other.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", other.transform.position.z);
        PlayerPrefs.SetInt("PlayerHealth", other.GetComponent<player_health>().currentHealth);
        // Place GetHolding and GetItem here
        /*if (other.GetComponent<player_controls>().isHolding()) {
             held = other.GetComponent<player_controls>().GetItem();
          } else
          {
             held = null;
          }
        }
        */
    }

    void SaveTonyState()
    { 
        PlayerPrefs.SetFloat("TonyPosX", Tony.transform.position.x);
        PlayerPrefs.SetFloat("TonyPosY", Tony.transform.position.y);
        PlayerPrefs.SetFloat("TonyPosZ", Tony.transform.position.z);
        PlayerPrefs.SetInt("TonyAggression", Tony.GetComponent<Tony>().aggression);
    }

    public void LoadPlayerState()
    {
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"), PlayerPrefs.GetFloat("PlayerPosZ"));
        player.GetComponent<player_health>().currentHealth = PlayerPrefs.GetInt("PlayerHealth");
        // if (held != null) { player.GetComponent<player_controls>().Pickup(held); }
    }

    public void LoadTonyState()
    {
        if (Tony != null)
        {
            Tony.transform.position = new Vector3(PlayerPrefs.GetFloat("TonyPosX"), PlayerPrefs.GetFloat("TonyPosY"), PlayerPrefs.GetFloat("TonyPosZ"));
            Tony.GetComponent<Tony>().aggression = PlayerPrefs.GetInt("TonyAggression");
        }
    }

    private void OnEnable()
    {
        player_health.onDeath += OnPlayerDeath;
        player = GameObject.Find("Player");
        Tony = GameObject.Find("Tony");
        used = false;
    }

    private void OnDisable()
    {
        player_health.onDeath -= OnPlayerDeath;
    }

    void OnPlayerDeath()
    {
        Debug.Log("Reloading...");
        LoadPlayerState();
        LoadTonyState();
    }

    public void SetPrevDoor(GameObject o)
    {
        previousDoor = o;
    }
}
