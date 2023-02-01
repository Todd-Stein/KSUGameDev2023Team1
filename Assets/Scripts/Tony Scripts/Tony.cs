using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        aggression = 40;
    }

    // Update is called once per frame
    void Update()
    {
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

        agent.speed = speed; // Change speed as necessary
        if (checkGoals())
        {
            // Change currentGoal to next goal in goals[]
            if (goalIndex < goals.Length)
            {
                currentGoal = goals[++goalIndex].GetComponent<Transform>();
            } else
            {
                goalIndex = 0;
                currentGoal = goals[goalIndex].GetComponent<Transform>();
            }
            agent.destination = currentGoal.position;
        }
    }

    public void OnAlert(Collider other, int aggroIncrease)
    {
        aggression += aggroIncrease;
        currentGoal = other.gameObject.GetComponent<Transform>();
        agent.destination = currentGoal.position;
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
}
