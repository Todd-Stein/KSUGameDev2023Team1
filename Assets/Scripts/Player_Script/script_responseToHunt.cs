using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_responseToHunt : MonoBehaviour
{
    private Quaternion playerOriginalRot;
    private bool isNearTony;
    private bool isTurningAnimDone;
    private Transform lookAtPos;
    private GameObject vignette;

    [SerializeField]
    private float turnTotalTime = 2.0f;
    private float turnCurrentTime = 0.0f;

    private void Awake()
    {
        if(vignette == null)
        {
            vignette = transform.GetChild(0).GetChild(0).GetChild(3).gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerOriginalRot = Quaternion.identity;
        turnTotalTime *= 2.0f;

    }

    // Update is called once per frame
    void Update()
    {
        vignette.SetActive(isNearTony);
        if(!isTurningAnimDone)
        {
            turnCurrentTime += Time.deltaTime / turnTotalTime;
            if(turnCurrentTime<turnTotalTime/2)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookAtPos.position, Vector3.up), turnCurrentTime);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, playerOriginalRot, turnCurrentTime);
            }
            if(turnCurrentTime>=turnTotalTime)
            {
                isTurningAnimDone = true;
            }

        }
    }
    public void EnableResponseToHuntMode(Transform lookAtLoc)
    {
        Debug.Log("Looking at tony engadged");
        isNearTony = true;
        isTurningAnimDone = false;
        lookAtPos = lookAtLoc;

    }
    public void DisableResponseToHuntMode()
    {
        Debug.Log("Done with look");
        isNearTony = false;
        isTurningAnimDone = true;
        turnCurrentTime = 0.0f;
    }
}
