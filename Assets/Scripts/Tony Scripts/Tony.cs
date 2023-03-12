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
    public bool hunting; // To be toggled when he is in hunt mode
    public bool alerted; // To be toggled when noise can/is heard by him
    private bool playerHasResponded = false;
    public float speed;  // Changes the speed that Tony moves
    public const float baseSpeed = 4;   // Base speed for Tony when not alerted
    public Transform currentGoal; // The current goal for Tony to move to
    public Transform[] goals; // An array of goals for Tony to move to
    public int goalIndex;

    public GameObject soundSphere; // Tony's listening range, in the form of a sphere

    public int aggression; // Tony's current aggression level.

    public float idleTimer;
    public float huntTimer;
    private float dmgCooldown;

    private GameObject playerRef;

    NavMeshAgent agent;
    Transform personalTransform;

    // Start is called before the first frame update
    void Start()
    {
        hunting = false;
        alerted = false;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = currentGoal.position;
        personalTransform = GetComponent<Transform>();
        goalIndex = 0;
        idleTimer = 0;
        dmgCooldown = 0;
        aggression = 40;
        if(playerRef == null)
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer();

        agent.speed = speed; // Change speed as necessary

        if (checkGoals())
        {
            // Change currentGoal to next goal in goals[]
            if (goalIndex < goals.Length)
            {
                //currentGoal = goals[++goalIndex].GetComponent<Transform>();
                changeGoal(goals[++goalIndex].GetComponent<Transform>());
            } else
            {
                goalIndex = 0;
                //currentGoal = goals[goalIndex].GetComponent<Transform>();
                changeGoal(goals[goalIndex].GetComponent<Transform>());
            }
            //agent.destination = currentGoal.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") &&
            !hunting)
        {
            GameObject player = other.GetComponent<GameObject>();
            OnTheHunt(player);
        } else if (other.gameObject.CompareTag("Player"))
        {
            playerHit(other);
            playerHasResponded = false;
        }
    }

    //Tony heads towards source of sound
    public void OnAlert(GameObject other, int aggro)
    {
        aggroIncrease(aggro);
        changeGoal(other.transform); // Changes destination to object that alerted Tony
        alerted = true;
        agent.autoBraking = true; // Enable autobraking for distractables
    }

    public void OnTheHunt(GameObject other)
    {
        //activated by contact or seen with goggles active
        //increased speed for 10 secs after hunting == false

        //increased speed and aggression
        //after 1.5: hunting  == false
        //after 10: speed is normal

        agent.autoBraking = false;

        if (!playerHasResponded)
        {
            playerRef.GetComponent<script_responseToHunt>().EnableResponseToHuntMode(transform);
            playerHasResponded = true;
        }

        huntTimer = 11.5f;
        hunting = true;        

        speed = aggression / 5;   //doubles current speed
        
        Debug.Log("Hunting");
        
        changeGoal(other.transform);
    }

    private void Timer()
    {
        if(idleTimer > 0)
            idleTimer -= Time.deltaTime;
        else
        {
            speed = aggression / 10;
            idleTimer = 0;
        }

        if (dmgCooldown > 0)
            dmgCooldown -= Time.deltaTime;

        if (huntTimer > 0)
            huntTimer -= Time.deltaTime;

        if(hunting && huntTimer <= 10f)
        {
            Debug.Log("No More Hunt");
            hunting = false;
            speed = aggression / 10;
            playerRef.GetComponent<script_responseToHunt>().DisableResponseToHuntMode();
            playerHasResponded = false;
        }
    }

    bool checkGoals()
    {
        if (currentGoal.position.x == personalTransform.position.x &&
            currentGoal.position.z == personalTransform.position.z && 
            !alerted && !hunting)
        {
            speed = 0;
            idleTimer = (float)((100 - aggression)/10); // Waits idle for less time as aggression increases
            goalIndex = Random.Range(0, goals.Length);
            return true;

        } else if(alerted && agent.remainingDistance <= 4f)
        {
            alerted = false;
            speed = 0;
            idleTimer = 2f; // Waits idle for less time as aggression increases
            goalIndex = Random.Range(0, goals.Length);
            agent.autoBraking = false;
            return true;
        } /*else if (hunting && agent.remainingDistance <= 1f)
        {
            speed = 0;
            idleTimer = (float)((100 - aggression) / 10);
            goalIndex = Random.Range(0, goals.Length); // Should probably change to still be player
            return true;
        }*/
        return false;
    }

    void changeGoal(Transform t)
    {
        currentGoal = t;
        agent.destination = currentGoal.position;
    }

    public void aggroIncrease(int value)
    {
        aggression += value;
        if (hunting)
        {
            speed = aggression / 5;
        }
        else
        {
            speed = aggression / 10; // Changes speed
        }

        // Changes sound sphere size here
        soundSphere.transform.localScale = new Vector3(aggression, aggression, aggression);
    }

    void playerHit(Collider other)
    {
        if (dmgCooldown <= 0)
        {
            // Play hit animation here
            other.GetComponent<player_health>().RecieveHit();
            dmgCooldown = 0.5f;
            speed = 0;
            idleTimer = 0.5f;
        }
    }
}
