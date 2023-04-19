using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goggles : MonoBehaviour
{

    private GameObject[] goggleobjs;
    private GameObject[] nongogobjs;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GoggleStart");
        //if(goggleobjs == null)
        //{
        Debug.Log("nongog count = " + GameObject.FindGameObjectsWithTag("nongog").Length);

        goggleobjs = GameObject.FindGameObjectsWithTag("Goggle");

            /*
            foreach (var obj in GameObject.FindGameObjectsWithTag("Goggle"))
            {
                goggleobjs.Add(obj);
            }
            */
        //}
        foreach (var obj in goggleobjs)
        {
            obj.GetComponent<MeshRenderer>().enabled = false;
            //Debug.Log("Disabling rend of" + obj.ToString());
        }
        //if (nongogobjs == null)
        //{
            
            //nongogobjs = new List<GameObject>();
            /*foreach (var obj in GameObject.FindGameObjectsWithTag("nongog")){
                nongogobjs.Add(obj);
                Debug.Log(obj.ToString());
            }*/
        nongogobjs = GameObject.FindGameObjectsWithTag("nongog");
        Debug.Log("nongog count = " + nongogobjs.Length);
        //}
    }

    public void Activate()
    {
        Debug.Log("GOGGLES ON!");
        Debug.Log("Activate nongog count " + nongogobjs.Length);
        for (int i = 0; i < nongogobjs.Length; ++i)
        {
            Debug.Log("Disableing");
            nongogobjs[i].GetComponent<MeshRenderer>().enabled = false;
        }

        
        for(int i = 0; i < goggleobjs.Length; ++i)
        {
            //Debug.Log("enabling " + obj.name);
            //obj.GetComponent<MeshRenderer>().enabled = true;
            goggleobjs[i].GetComponent<MeshRenderer>().enabled = true;
        }

        
    }

    public void Disable()
    {
        Debug.Log("GOGGELS OFF");
        for (int i = 0; i < nongogobjs.Length; ++i)
        {
            nongogobjs[i].GetComponent<MeshRenderer>().enabled = true;
        }
        for (int i = 0; i < goggleobjs.Length; ++i)
        {
            
            goggleobjs[i].GetComponent<MeshRenderer>().enabled = false;
        }
        
    }
}
