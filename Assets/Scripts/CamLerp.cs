using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLerp : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;

    private Vector3 smoothPos;
    public Vector3 targetPos;

    private Quaternion smoothRot;
    public Quaternion targetRot;

    public bool LERP;
    public float smoothSpeed;

    private void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (LERP)
        {
            smoothPos = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
            transform.position = smoothPos;

            smoothRot = Quaternion.Lerp(transform.rotation, targetRot, smoothSpeed);
            transform.rotation = smoothRot;
        }
    }

    public void ResetCam()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = startRot;
        LERP = false;
    }
}
