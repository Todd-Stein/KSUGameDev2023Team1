using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tony : MonoBehaviour
{
    public bool hunting; // To be toggled when he is in hunt mode
    public bool alerted; // To be toggled when noise can/is heard by him
    public float speed;  // Changes the speed that Tony moves
    public Transform currentGoal; // The current goal for Tony to move to
    public Transform[] goals; // An array of goals for Tony to move to
    int goalIndex;

    public int aggression; // Tony's current aggression level.

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
    }

    // Update is called once per frame
    void Update()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
    }

    bool checkGoals()
    {
        if (currentGoal.position.x == personalTransform.position.x &&
            currentGoal.position.z == personalTransform.position.z)
        {
            return true;
        }
        return false;
    }
}
