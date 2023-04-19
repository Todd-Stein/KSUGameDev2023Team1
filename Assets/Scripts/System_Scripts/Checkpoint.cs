using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Checkpoint : MonoBehaviour
{
    public GameObject nextGoals;   // Tony's collection of next goals, an array of patrol points he will switch to when the Checkpoint is passed.
    public GameObject Tony;         // Tony itself
    public GameObject player;       // Reference to the player
    GameObject held;                // GameObject player was holding when they passed through the Checkpoint

    [SerializeField]
    GameObject nextDoor;            // Next Checkpoint in scene
    [SerializeField]
    GameObject previousDoor;        // Last Checkpoint in scene

    //[SerializeField]
    static private bool used;       // Mark whether the Checkpoint has been passed or not

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<MeshRenderer>().enabled = false;
        Tony = GameObject.Find("Tony"); // Tony is usually named 'Tony'
        //player = GameObject.Find("Player"); // Player is not necessarily named 'Player.' Have to manually drag player into Checkpoint object
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !used)    // If the player passes through the Checkpoint...
        {
            Debug.Log("Passed Checkpoint.");        // Log on the console they passed
            used = true;                            // Mark this checkpoint as having been passed through
            SavePlayerState(other.gameObject);      // Call the function to save the player's current state
            if (Tony != null) { SaveTonyState(); }  // If Tony exists, save his state, too, through the function

            if (nextGoals != null && Tony != null)  // If there are patrol points in the next area, and tony exists...
            {
                Tony.GetComponent<Tony>().goals.Clear();
                foreach (Transform pnt in nextGoals.GetComponentsInChildren<Transform>())
                {
                    Tony.GetComponent<Tony>().goals.Add(pnt); // Assign Tony's next patrol points
                }
                Tony.GetComponent<Tony>().goalIndex = 0;     // Reset Tony's current patrol point
            }

            if (nextDoor != null)                   // If there is a Checkpoint after this one...
            {
                nextDoor.SetActive(true);           // Activate the Checkpoint after this one
                nextDoor.GetComponent<Checkpoint>().SetPrevDoor(gameObject); // Set this GameObject as the next Checkpoint's previous checkpoint.
                //gameObject.SetActive(false);
            }

            if (previousDoor != null)               // If there was a previous Checkpoint...
            {
                previousDoor.SetActive(false);      // Disable that Checkpoint.
            }
        }
    }

    void SavePlayerState(GameObject other) // Function to save the player state
    {
        PlayerPrefs.SetFloat("PlayerPosX", other.transform.position.x); // Save the player's x position
        PlayerPrefs.SetFloat("PlayerPosY", other.transform.position.y); // Save the player's y position
        PlayerPrefs.SetFloat("PlayerPosZ", other.transform.position.z); // Save the player's z position
        PlayerPrefs.SetInt("PlayerHealth", other.GetComponent<player_health>().currentHealth); // Save the player's health
        // Place GetHolding and GetItem here
        if (other.GetComponent<player_controls>().GetHolding()) { // If the player is holding an item...
            held = other.GetComponent<player_controls>().GetItem(); // Retrieve the held item
        } else
        {
            held = null; // If not, set the held item to nothing
        }
    }

    void SaveTonyState() // Function to save Tony's state
    { 
        PlayerPrefs.SetFloat("TonyPosX", Tony.transform.position.x); // Save Tony's x position
        PlayerPrefs.SetFloat("TonyPosY", Tony.transform.position.y); // Save Tony's y position
        PlayerPrefs.SetFloat("TonyPosZ", Tony.transform.position.z); // Save Tony's z position
        PlayerPrefs.SetInt("TonyAggression", Tony.GetComponent<Tony>().aggression); // Save Tony's current aggression
    }

    public void LoadPlayerState() // Function to load the player's save state
    {
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"), PlayerPrefs.GetFloat("PlayerPosZ")); // Set the player to the Checkpoint's saved position
        player.GetComponent<player_health>().currentHealth = PlayerPrefs.GetInt("PlayerHealth"); // Set the player's health to the saved health
        if (held != null) { player.GetComponent<player_controls>().Pickup(held); } // Set the player's item to the saved item
    }

    public void LoadTonyState() // Function to load Tony's save state
    {
        if (Tony != null) // If Tony exists...
        {
            Tony.transform.position = new Vector3(PlayerPrefs.GetFloat("TonyPosX"), PlayerPrefs.GetFloat("TonyPosY"), PlayerPrefs.GetFloat("TonyPosZ")); // Set Tony's position to the saved position
            Tony.GetComponent<Tony>().aggression = PlayerPrefs.GetInt("TonyAggression"); // Set Tony's aggression to the saved aggression level
        }
    }

    private void OnEnable() // Function called when this Checkpoint is enabled in the scene
    {
        player_health.onDeath += OnPlayerDeath; // Subscribe the event 'onDeath' to run when OnPlayerDeath runs
        //player = GameObject.Find("Player");     // Finds the player in the scene. Player is not always named 'Player'
        Tony = GameObject.Find("Tony"); // Finds Tony in the scene
        used = false; // Sets this Checkpoint to not having been passed through
    }

    private void OnDisable() // Function called when this Checkpoint is disabled in the scene
    {
        player_health.onDeath -= OnPlayerDeath; // Unsubscribe onDeath from OnPlayerDeath
    }

    void OnPlayerDeath() // Function called when player dies
    {
        Debug.Log("Reloading...");
        LoadPlayerState(); // Run LoadPlayerState
        LoadTonyState();   // Run LoadTonyState
    }

    public void SetPrevDoor(GameObject o) // Function called to set the previous Checkpoint to the previous Checkpoint
    {
        previousDoor = o;
    }
}
