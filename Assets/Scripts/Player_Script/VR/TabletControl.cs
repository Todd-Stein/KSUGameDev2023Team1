using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class TabletControl : MonoBehaviour
{
    public SteamVR_Action_Boolean use;
    private Interactable interActable;

    public MeshRenderer tabletMesh;
    public Material cameraFeed;
    public Material black;
    public Material booting0;
    public Material booting1;
    public Material booting2;
    public Material booting3;
    private Material[] mats;
    public float animationDuration = 3f;

    private bool powerState = false;
    private bool slowdown = false;

    public Goggles gameManager;

    void Start()
    {
        powerState = false;
        //start with material set to black
        tabletMesh = GetComponent<MeshRenderer>();
        ChangeMat(black);

        gameManager = GameObject.Find("GameManager").GetComponent<Goggles>();

        interActable = GetComponent<Interactable>();

    }

    private void ChangeMat(Material mat)
    {
        mats = tabletMesh.materials;
        mats[1] = mat;
        tabletMesh.materials = mats;
    }

    private void Update()
    {
        if (interActable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interActable.attachedToHand.handType;
            if (use[source].stateDown && !slowdown)
            {
                

                slowdown = true;
                StartCoroutine(Slowdowntimer());

                if (!powerState)
                    TurnOnTablet();
                else if (powerState)
                    TurnOffTablet();
            }
        }
    }
    IEnumerator Slowdowntimer()
    {
        yield return new WaitForSeconds(0.3f);
        slowdown = false;
    }

    void TurnOnTablet()
    {
        powerState = true;

        StartCoroutine(BootAni());
    }

    IEnumerator BootAni()
    {
        if (!powerState) yield break;
        if (powerState)
        {
            ChangeMat(booting0);
            if (!powerState) yield break;

            yield return new WaitForSeconds(1f);
            if (!powerState) yield break;

            ChangeMat(booting1);
            if (!powerState) yield break;

            yield return new WaitForSeconds(1f);
            if (!powerState) yield break;

            ChangeMat(booting2);
            if (!powerState) yield break;

            yield return new WaitForSeconds(1f);
            if (!powerState) yield break;

            ChangeMat(booting3);
            if (!powerState) yield break;

            yield return new WaitForSeconds(1f);

            if (powerState)
            {
                ChangeMat(cameraFeed);
                gameManager.Activate();
            }
        }
    }

    void TurnOffTablet()
    {
        powerState = false;
        gameManager.Disable();
        ChangeMat(black);
    }
}
