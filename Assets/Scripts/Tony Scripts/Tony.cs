using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//throwing notes here to keep better track of things:
//tony's speed and hearing range is proportional to aggression level 
//higher level sound events takes priority over lower ones
//hunt mode is 1.5 secs, might need to adjust in the future
//hunt mode activated by player touching tony or being seen with goggles 
//after exiting hunting mode, heightened aggression and hearing range
//--probably make range based off of aggression
//increased speed for 10 secs after exiting hunting
//destination reached timer dependant on noise level and aggression
//aggression decreases over time

public class Tony : MonoBehaviour
{
    public bool hunting;              // To be toggled when he is in hunt mode
    public bool alerted;              // To be toggled when noise can/is heard by him
    private bool playerHasResponded = false;
    public float speed;               // Changes the speed that Tony moves
    public const float baseSpeed = 4; // Base speed for Tony when not alerted
    public Transform currentGoal;     // The current goal for Tony to move to
    public List<Transform> goals;     // An array of goals for Tony to move to
    public int goalIndex;
    public GameObject initialGoals;   // A parent gameobject of each tony goal, initial to the scene

    public GameObject soundSphere;    // Tony's listening range, in the form of a sphere

    public int aggression;  // Tony's current aggression level.

    public float idleTimer;
    bool idle;              // Whether Tony is idle or not
    public float huntTimer;
    bool huntBegin;
    public bool canHunt;

    public bool attacking = false;

    public GameObject playerRef;

    NavMeshAgent agent;
    Transform personalTransform;
    //private GameObject playerRef;
    Animator ani;
    [SerializeField]
    AudioSource roar;

    // Start is called before the first frame update
    void Start()
    {
        hunting = false;
        huntBegin = false;
        alerted = false;
        idle = false;
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        personalTransform = GetComponent<Transform>();
        roar = GetComponent<AudioSource>();
        goalIndex = 0;
        idleTimer = 0;
        aggression = 20;
        speed = aggression / 10;
        soundSphere.transform.localScale = new Vector3(aggression, aggression, aggression);
        if (playerRef == null)
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
        }
        foreach (Transform pnt in initialGoals.GetComponentsInChildren<Transform>()) // Iterate through the children of the initialGoals GameObject and add their transforms as patrol points
        {
            goals.Add(pnt); // Assign Tony's first patrol points
        }
        currentGoal = goals[Random.Range(0, goals.Count)];
        agent.destination = currentGoal.position;
        canHunt = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Call all our timers
        Timer();

        if (agent.isActiveAndEnabled)
        {
            agent.speed = speed; // Change speed as necessary
        }

        if (checkGoals() && !hunting) // If he has reached an alert or patrol point, and is not hunting the player
        {
            try { changeGoal(goals[Random.Range(0, goals.Count)]); }
            catch { }
        } else if (hunting && canHunt && agent.isActiveAndEnabled && Vector3.Distance(playerRef.transform.position, transform.position) <= 3f && attacking == false) { // If Tony is hunting and the player is within 1 units...
            ani.SetTrigger("swipe"); // Play damage animation, which calls the PlayerHit() function.
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Player") &&
            !hunting && canHunt)
        {
            GameObject player = collision.gameObject;
            OnTheHunt(player);
        }
    }

    //Tony heads towards source of sound
    public void OnAlert(GameObject other, int aggro)
    {
        aggroIncrease(aggro);
        changeGoal(other.transform); // Changes destination to object that alerted Tony
        alerted = true;
        agent.autoBraking = true;    // Enable autobraking for distractables
    }

    public void OnTheHunt(GameObject other)
    {
        //activated by contact or seen with goggles active
        //increased speed for 10 secs after hunting == false

        //increased speed and aggression
        //after 10: speed is normal
        if (!hunting && huntTimer <= 0 && canHunt)
        {
            hunting = true;
            huntTimer = 11.5f;
            agent.autoBraking = false; // Tony does not slow down as he approaches goal
            changeGoal(other.transform);
            agent.enabled = false;
            roar.Play();
            ani.SetTrigger("rage");

            if (!playerHasResponded) // If player response has not already played...
            {
                playerRef.GetComponent<script_responseToHunt>().EnableResponseToHuntMode(transform); // Play player response
                playerHasResponded = true; // Confirm the player has responded now
            }

            Debug.Log("Hunting");
        }
    }

    private void Timer()
    {
        if (idleTimer > 0 && idle == true)
        {
            if (!ani.GetBool("idle")) { ani.SetBool("idle", true); ani.SetBool("walk", false); } // Show him as idle if not already
            speed = 0;
            idleTimer -= Time.deltaTime;
        }
        else if (idleTimer <= 0 && idle == true)
        {
            speed = aggression / 10;
            idleTimer = 0;
            idle = false;
            ani.SetBool("idle", false);
            ani.SetBool("walk", true);
            changeGoal(goals[Random.Range(0, goals.Count)]);
        }

        if (huntTimer > 0 && hunting && canHunt)
        {
            huntTimer -= Time.deltaTime;
            changeGoal(playerRef.transform);    // change the goal to the player, as they are (theoretically) always moving
        } 
        else if (hunting && canHunt)
        {
            hunting = false;            // No longer hunting
            huntTimer = 0;              // Set hunt timer to 0
            speed = aggression / 10;    // Reset to default speed
            changeGoal(goals[Random.Range(0, goals.Count)]); // Random patrol point
        }
    }

    bool checkGoals()
    {
        if (agent.isActiveAndEnabled && agent.remainingDistance <= 0f &&
            !alerted && !hunting) // If Tony is simply patrolling, and has reached a patrol point...
        {
            //ani.SetBool("walk", false);
            //ani.SetBool("idle", true);
            //speed = 0;
            //idleTimer = (float)((100 - aggression) / 10); // Waits idle for less time as aggression increases
            goalIndex = Random.Range(0, goals.Count);   // Get a new patrol point
            return true;
        }
        else if (agent.isActiveAndEnabled && !hunting && alerted && agent.remainingDistance <= 4f) // If Tony was alerted by an object and reached it...
        {
            alerted = false;                            // No longer alert
            speed = 0;                                  // Waits idle for a time
            idleTimer = 2f;                             // Waits idle
            idle = true;
            ani.SetBool("idle", true);                  // Animation set to idle animation
            ani.SetBool("walk", false);
            //goalIndex = Random.Range(0, goals.Count);   // Set goal index to random goal - does nothing
            agent.autoBraking = false;                  // Turn off autobraking so Tony doesn't slow when reaching goal
            return true;
        }
        return false;
    }

    void changeGoal(Transform t)
    {
        if (agent.isActiveAndEnabled)
        {
            currentGoal = t;
            agent.destination = currentGoal.position;
        } else
        {
            currentGoal = t;
        }
    }

    public void aggroIncrease(int value)
    {
        aggression += value;

        // Changes sound sphere size here
        soundSphere.transform.localScale = new Vector3(aggression, aggression, aggression);
    }

    // Function called by animator when the animation is at the right spot to hit player
    public void playerHit()
    {
        Debug.Log("hit");
        //if (Vector3.Distance(playerRef.transform.position, transform.position) <= 4f)
        //{
        if(playerRef.GetComponent<player_health>().currentHealth == 0)
        {
            return;
        }
        playerRef.GetComponent<player_health>().RecieveHit();
        //}
    }

    // End the hunt early (player dies, game ends, etc)
    public void EndHunt() 
    {
        hunting = false;
        huntBegin = false;
        huntTimer = 0f;
        speed = aggression / 10;
        playerHasResponded = false;
        ani.ResetTrigger("rage");
        changeGoal(goals[Random.Range(0, goals.Count)]);
    }

    public void Unfreeze()
    {
        agent.enabled = true;
        hunting = true;
        huntTimer = 10f;
        
        aggroIncrease(hunting == true ? 0 : 1); // Increase aggression slightly

        speed = aggression / 8; // Slightly increase speed instead of the previous massive increase

        playerRef.GetComponent<script_responseToHunt>().DisableResponseToHuntMode();
        playerHasResponded = false; // Player response finished
    }

    public void attackswitch()
    {
        
        attacking = !attacking;
        if (attacking)
        {
            agent.enabled = false;
        }
        else
        {
            agent.enabled = true;
        }
    }
}