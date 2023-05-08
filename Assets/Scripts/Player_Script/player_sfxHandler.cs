using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public enum groundMaterialType {
    concrete,
    wood,
    metal
}
[System.Serializable]
public class AudioClipSerialize
{
    public List<AudioClip> clip;
}

public class player_sfxHandler : MonoBehaviour
{
    public groundMaterialType currentGroundMat;
    private Rigidbody rb;
    public AudioSource audioSRCR, audioSRCL;
    [SerializeField]
    //public List<List<AudioClip>> footstepSounds;
    public List<AudioClip> footsteps;
    private RaycastHit hit;

    public float timeBetweenSteps = .2f;
    public float currentTimeBetweenSteps = 0.0f;

    private Vector3 currentLoc, prevLoc;

    public Transform joint;
    private Vector3 jointOriginalPos;
    private bool Right = false;
    private bool step = false;

    private void Awake()
    {
        audioSRCR = GetComponents<AudioSource>()[0];
        audioSRCL = GetComponents<AudioSource>()[1];
        rb = GetComponent<Rigidbody>();
        jointOriginalPos = joint.localPosition;
        //footstepSounds = new List<List<AudioClip>>();
    }

    // Start is called before the first frame update
    void Start()
    {
        prevLoc = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Joint loc pos = " + jointOriginalPos.y);
        if (Input.GetKeyUp(KeyCode.W))
        {
            currentTimeBetweenSteps = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            audioSRCR.clip = footsteps[2];
            audioSRCL.clip = footsteps[3];
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            audioSRCR.clip = footsteps[0];
            audioSRCL.clip = footsteps[1];
        }

        if (joint.localPosition.y <= jointOriginalPos.y - .08f)
        {
            //Debug.Log("Play Sound");
            if (!Right && !step)
            {
                audioSRCR.Play();
                Right = true;
                step = true;
            }
            else
            {
                if (!step)
                {
                    audioSRCL.Play();
                    Right = false;
                    step = true;
                }
            }

        }
        if(joint.localPosition.y > jointOriginalPos.y - .08f){
            step = false;
        }
        /*
        currentLoc = transform.position;
        Debug.DrawRay(transform.position, transform.up * -2.0f);
        //Debug.Log(rb.velocity.magnitude);
        //if(Physics.Raycast(transform.position, transform.up*-1.0f, out hit, 2.0f))
        //{
            //Debug.Log(rb.velocity.sqrMagnitude>0);
            if ((currentLoc - prevLoc).magnitude > 0.01f)
            {
                //Debug.Log(rb.velocity.sqrMagnitude);
                AudioClipSerialize temp = footsteps[(int)currentGroundMat];
                //audioSRC.clip = temp.clip[Random.Range(0, temp.clip.Count)];
                currentTimeBetweenSteps += Time.deltaTime;
                if (!audioSRC.isPlaying)
                {
                    if (currentTimeBetweenSteps >= timeBetweenSteps)
                    {
                        AudioClip tempClip = temp.clip[4];
                        currentTimeBetweenSteps = 0.0f;
                        //timeBetweenSteps = tempClip.length;
                        audioSRC.PlayOneShot(tempClip);
                    }
                }
                //AudioSource.PlayClipAtPoint(temp.clip[Random.Range(0, temp.clip.Count)], transform.position);
            //}
            //else
                //currentTimeBetweenSteps = 0.0f;
                //audioSRC.Stop();
        }
        else
        {
            //audioSRC.Stop();
        }
        prevLoc = currentLoc;
        */
    }
    void FootstepsSFX()
    {
        //audioSRC.pitch = Mathf.Max(rb.velocity.magnitude, 1.0f);
        //audioSRC.clip = footstepSounds[(int)currentGroundMat][Random.Range(0, footstepSounds.Count)];
        
    }


}
