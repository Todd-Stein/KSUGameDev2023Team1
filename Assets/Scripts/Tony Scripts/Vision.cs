using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public bool goggs = true;

    public GameObject curHitObj;

    public float radius;
    public float distance;
    public LayerMask layer;

    private Vector3 origin;
    private Vector3 dir;
    private float curHitDis;


    [SerializeField]
    GameObject tony;

    private void Update()
    {
        if (curHitObj.CompareTag("Player") && !goggs)
        {
            tony.GetComponent<Tony>().OnAlert(curHitObj, 10);
            Debug.Log("Alerted");
        }

        if (curHitObj.CompareTag("Player") && goggs)
        {
            tony.GetComponent<Tony>().OnTheHunt(curHitObj);
            Debug.Log("Hunting");
        }
    }

    private void FixedUpdate()
    {
        origin = transform.position;
        dir = transform.forward;
        RaycastHit hit;
        if (Physics.SphereCast(origin, radius, dir, out hit, distance, layer, QueryTriggerInteraction.UseGlobal))
        {
            curHitObj = hit.transform.gameObject;
            curHitDis = hit.distance;
        }
        else
        {
            curHitDis = distance;
            curHitObj = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + dir * curHitDis);
        Gizmos.DrawWireSphere(origin + dir * curHitDis, radius);
    }
}
