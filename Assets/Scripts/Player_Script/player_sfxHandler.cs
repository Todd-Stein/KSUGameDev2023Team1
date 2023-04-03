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
    private AudioSource audioSRC;
    [SerializeField]
    //public List<List<AudioClip>> footstepSounds;
    public List<AudioClipSerialize> footsteps;
    private RaycastHit hit;

    private float timeBetweenSteps = 0.0f;
    private float currentTimeBetweenSteps = 0.0f;

    private Vector3 currentLoc, prevLoc;

    private void Awake()
    {
        audioSRC = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();   
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
        currentLoc = transform.position;
        Debug.DrawRay(transform.position, transform.up * -2.0f);
        //Debug.Log(rb.velocity.magnitude);
        if(Physics.Raycast(transform.position, transform.up*-1.0f, out hit, 2.0f))
        {
            //Debug.Log(rb.velocity.sqrMagnitude>0);
            if ((currentLoc - prevLoc).magnitude > 0.0f)
            {
                //Debug.Log(rb.velocity.sqrMagnitude);
                AudioClipSerialize temp = footsteps[(int)currentGroundMat];
                //audioSRC.clip = temp.clip[Random.Range(0, temp.clip.Count)];
                currentTimeBetweenSteps += Time.deltaTime;
                if (!audioSRC.isPlaying)
                {
                    if (currentTimeBetweenSteps >= timeBetweenSteps)
                    {
                        AudioClip tempClip = temp.clip[Random.Range(0, temp.clip.Count)];
                        currentTimeBetweenSteps = 0.0f;
                        timeBetweenSteps = tempClip.length;
                        audioSRC.PlayOneShot(tempClip);
                    }
                }
                //AudioSource.PlayClipAtPoint(temp.clip[Random.Range(0, temp.clip.Count)], transform.position);
            }
            else
                currentTimeBetweenSteps = 0.0f;
                //audioSRC.Stop();
        }
        else
        {
            //audioSRC.Stop();
        }
        prevLoc = currentLoc;
    }
    void FootstepsSFX()
    {
        //audioSRC.pitch = Mathf.Max(rb.velocity.magnitude, 1.0f);
        //audioSRC.clip = footstepSounds[(int)currentGroundMat][Random.Range(0, footstepSounds.Count)];
        
    }
}
