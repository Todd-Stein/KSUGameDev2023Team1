using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goggles : MonoBehaviour
{

    private List<GameObject> goggleobjs;
    private List<GameObject> nongogobjs;
    
    // Start is called before the first frame update
    void Start()
    {
        if(goggleobjs == null)
        {
            goggleobjs = new List<GameObject>();
            foreach (var obj in GameObject.FindGameObjectsWithTag("Goggle"))
            {
                goggleobjs.Add(obj);
            }
        }
        foreach (var obj in goggleobjs)
        {
            obj.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log(obj.ToString());
        }
        if (nongogobjs == null)
        {
            Debug.Log("nongog");
            nongogobjs = new List<GameObject>();
            foreach (var obj in GameObject.FindGameObjectsWithTag("nongog")){
                nongogobjs.Add(obj);
                Debug.Log(obj.ToString());
            }
        }
    }

    public void Activate()
    {
        foreach (var obj in goggleobjs)
        {
            obj.GetComponent<MeshRenderer>().enabled = true;
        }
        foreach (var obj in nongogobjs)
        {
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void Disable()
    {
        foreach (var obj in goggleobjs)
        {
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
        foreach (var obj in nongogobjs)
        {
            obj.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
