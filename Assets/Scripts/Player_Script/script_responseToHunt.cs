using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class script_responseToHunt : MonoBehaviour
{
    private Quaternion playerOriginalRot;

    private Quaternion tonyLookAt;

    private bool isNearTony = false;
    private bool isTurningAnimDone;
    private GameObject vignette;

    private FirstPersonController controls1;
    private player_controls controls2;

    private bool reverseTurn = false;

    [SerializeField]
    private float turnTotalTime = 2.0f;
    [SerializeField]
    private float turnCurrentTime = 0.0f;

    [SerializeField]
    public PostProcessVolume vol;
    [SerializeField]
    private bool hunt;
    private Vignette postVig;
    private ChromaticAberration postChrom;

    public float vignetVal;
    public float ChromVal;

    public AudioSource hbeat;

    private void Awake()
    {
        if(vignette == null)
        {
            vignette = transform.GetChild(0).GetChild(0).GetChild(3).gameObject;
        }
        isNearTony = false;

        isTurningAnimDone = true;
        controls1 = GetComponent<FirstPersonController>();
        controls2 = GetComponent<player_controls>();


        vol.profile.TryGetSettings<Vignette>(out postVig);
        vol.profile.TryGetSettings<ChromaticAberration>(out postChrom);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerOriginalRot = Quaternion.identity;

    }

    // Update is called once per frame
    void Update()
    {


        //vignette.SetActive(isNearTony);

        if (hunt)
        {
            if (postVig.intensity.value <= vignetVal)
            {
                postVig.intensity.value += 1f * Time.deltaTime;
            }
            if(postChrom.intensity.value <= ChromVal)
            {
                postChrom.intensity.value += 2f * Time.deltaTime;
            }

        }
        if (!hunt)
        {
            if (postVig.intensity.value >= 0)
            {
                postVig.intensity.value -= 1f * Time.deltaTime;
            }
            if (postChrom.intensity.value >= 0)
            {
                postChrom.intensity.value -= 2f * Time.deltaTime;
            }
            if(postVig.intensity.value <= 0)
            {
                postVig.intensity.value = 0;
                //postVig.active = false;
            }
            if(postChrom.intensity.value <= 0)
            {
                postVig.intensity.value = 0;
                //postVig.active = false;
            }
        }

       // controls1.enabled = isTurningAnimDone;
       //controls2.enabled = isTurningAnimDone;
       
        /*
        if(!isTurningAnimDone)
        {
            turnCurrentTime += Time.deltaTime / turnTotalTime;
            if (turnCurrentTime < turnTotalTime && !reverseTurn)
            {
                Debug.Log("look at tony");
                transform.rotation = Quaternion.Lerp(playerOriginalRot, tonyLookAt, turnCurrentTime);
            }
            else if (turnCurrentTime > turnTotalTime && !reverseTurn)
            {
                reverseTurn = true;
                turnCurrentTime = 0.0f;
            }
            if(turnCurrentTime<turnTotalTime && reverseTurn)
            {
                Debug.Log("turn back");
                transform.rotation = Quaternion.Lerp(tonyLookAt, playerOriginalRot, turnCurrentTime);
            }
            if(turnCurrentTime>=turnTotalTime && reverseTurn)
            {
                isTurningAnimDone = true;
                DisableResponseToHuntMode();
            }
        }
        */
    }
    public void EnableResponseToHuntMode(Transform lookAtLoc)
    {
        hbeat.Play();
        hunt = true;
        
        //postVig.active = true;
        //postChrom.active = true;
        

        /*
        turnCurrentTime = 0.0f;
        Debug.Log("player response");
        isNearTony = true;
        isTurningAnimDone = false;
        playerOriginalRot = transform.rotation;
        tonyLookAt =  Quaternion.LookRotation(lookAtLoc.position, Vector3.up);
        */
    }
    public void DisableResponseToHuntMode()
    {
        hbeat.Stop();
        hunt = false;

        /*
        isNearTony = false;
        isTurningAnimDone = true;
        turnCurrentTime = turnTotalTime;
        */
    }
}
