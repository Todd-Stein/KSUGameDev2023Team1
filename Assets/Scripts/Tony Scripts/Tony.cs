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
    public float speed;  // Changes the speed that Tony moves
    public const float baseSpeed = 4;   // Base speed for Tony when not alerted
    public Transform currentGoal; // The current goal for Tony to move to
    public Transform[] goals; // An array of goals for Tony to move to
    int goalIndex;

    public int aggression; // Tony's current aggression level.

    private float timer;
    private float timerCompare;
    private float huntTimerCompare;
    private float huntTimer;

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
        timer = 0;
        huntTimer = 0;
        aggression = 40;
    }

    // Update is called once per frame
    void Update()
    {
        //general timer, mostly for reaching destinations
        if(timer <= timerCompare)
        {
            timer += Time.deltaTime;
        }
        else
        {
            speed = baseSpeed;
            timerCompare = 0;
            timer = 0;
        }

<<<<<<< Updated upstream
        //hunting timer, can be adjusted later
        if (huntTimer <= huntTimerCompare)
        {
            huntTimer += Time.deltaTime;
            //Debug.Log(timer);
        }
        else if (huntTimer >= 1.5f)
        {
            hunting = false;
            Debug.Log("No More Hunt");
        }
        else
        {
            Debug.Log("time over");
            //speed = speed before hunting
        }

        //agent.speed = speed; // Change speed as necessary

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

        if (hunting == true)
        {
            OnTheHunt();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnTheHunt();
        }
    }

    //Tony heads towards source of sound or player if in vision
    //need to shrink listening range
    //to make bigger when hunting
    public void OnAlert(Collider other, int aggro)
    {
        aggroIncrease(aggro);
        changeGoal(other.transform); // Should change destination to object that alerted Tony
    }

    public void OnTheHunt()
    {
        //activated by contact or seen with goggles active
        //hunting == true for 1.5 secs
        //increased speed for 10 secs after hunting == false

        //increased speed and aggression

        //timer = 11.5f;
        //after 1.5: hunting  == false
        //after 10: speed is normal

        huntTimerCompare = 11.5f;
        speed = aggression / 5;   //doubles current speed
        
        Debug.Log("Hunting");
    }

    bool checkGoals()
    {
        if (currentGoal.position.x == personalTransform.position.x &&
            currentGoal.position.z == personalTransform.position.z)
        {
            speed = 0;
            timerCompare = 2.0f;
            return true;
        }
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
        speed = aggression / 10; // Changes speed
        // Change sound sphere size here
    }
}
