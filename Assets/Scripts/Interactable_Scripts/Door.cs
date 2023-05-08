using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool unlocked = false;
    public bool opened = false;

    public AudioSource openSound;
    public AudioSource openDone;
    private Animator animator;
    public bool finalDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Door unlocked: " + unlocked.ToString());
        }

    }

    public void open()
    {
        if (unlocked)
        {
            if (finalDoor == true)
            {
                SceneManager.LoadScene("EndCredits");
            }
            animator.SetBool("Open", true);
            
        }
        
    }

    public void ForceOpen()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SoundEvent>().activateNoiseEvent();
    }

    public void playOpen()
    {
        openSound.Play();
    }

    public void OpenDone()
    {
        openDone.Play();
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        opened = true;
        GetComponent<NavMeshObstacle>().enabled = false;
    }
}
