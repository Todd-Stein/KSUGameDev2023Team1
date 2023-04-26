using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goggles : MonoBehaviour
{

    private GameObject[] goggleobjs;

    private Stack<GameObject> nongogobjs1;
    private Stack<GameObject> nongogobjs2;

    private GameObject[] nongogobjs;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GoggleStart");
        //if(goggleobjs == null)
        //{



        Debug.Log("nongog object count is: " + GameObject.FindGameObjectsWithTag("nongog").Length);
        nongogobjs1 = new Stack<GameObject>();
        nongogobjs2 = new Stack<GameObject>();
        goggleobjs = GameObject.FindGameObjectsWithTag("Goggle");

        Debug.Log("Now doing nongogloop!");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("nongog").Length; ++i)
        {

            nongogobjs1.Push(GameObject.FindGameObjectsWithTag("nongog")[i]);
            Debug.Log(GameObject.FindGameObjectsWithTag("nongog")[i].name);
        }
        nongogobjs = GameObject.FindGameObjectsWithTag("nongog");
        Debug.Log("nongog count = " + nongogobjs1.Count);
        //}

        Debug.Log("Unrendering all goggles");
        for (int i = 0; i < goggleobjs.Length; ++i)
        {
            goggleobjs[i].GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("Disabling rend of" + goggleobjs[i].name);
        }
;
        

        Debug.Log("end of goggle start!");
    }

    public void Activate()
    {
        Debug.Log("GOGGLES ON!");
        Debug.Log("Activate nongog count " + nongogobjs1.Count);
        /*if (nongogobjs1.Count > 0)
        {
            for (int i = 0; i < nongogobjs1.Count; ++i)
            {
                Debug.Log("Disableing " + nongogobjs1.Peek().name);
                try { nongogobjs1.Peek().GetComponent<MeshRenderer>().enabled = false; }
                catch { }
                nongogobjs2.Push(nongogobjs1.Pop());
            }
        }
        else
        {
            for (int i = 0; i < nongogobjs2.Count; ++i)
            {
                Debug.Log("Disableing");
                nongogobjs2.Peek().GetComponent<MeshRenderer>().enabled = false;
                nongogobjs1.Push(nongogobjs2.Pop());
            }
        }*/

        for(int i = 0; i < nongogobjs.Length; ++i)
        {
            try { nongogobjs[i].GetComponent<MeshRenderer>().enabled = false; }
            catch { }
        }

        
        for(int i = 0; i < goggleobjs.Length; ++i)
        {
            //Debug.Log("enabling " + obj.name);
            //obj.GetComponent<MeshRenderer>().enabled = true;
            try { goggleobjs[i].GetComponent<MeshRenderer>().enabled = true; }
            catch { Debug.Log(goggleobjs[i].name + " has no mesh renderer component"); }
            try { goggleobjs[i].GetComponent<SkinnedMeshRenderer>().enabled = true; }
            catch { }
        }

        
    }

    public void Disable()
    {
        Debug.Log("GOGGELS OFF");
        /*if (nongogobjs1.Count > 0)
        {
            for (int i = 0; nongogobjs1.Count != 0; ++i)
            {
                Debug.Log("Disableing");
                try { nongogobjs1.Peek().GetComponent<MeshRenderer>().enabled = true; }
                catch { }
                nongogobjs1.Push(nongogobjs2.Pop());
            }
        }
        else
        {
            for (int i = 0; i < nongogobjs2.Count; ++i)
            {
                Debug.Log("Disableing");
                nongogobjs2.Peek().GetComponent<MeshRenderer>().enabled = true;
                nongogobjs1.Push(nongogobjs2.Pop());
            }
        }*/

        for (int i = 0; i < nongogobjs.Length; ++i)
        {
            try { if (nongogobjs[i].GetComponent<Door>().opened == true) { continue; } }
            catch { }
            try { nongogobjs[i].GetComponent<MeshRenderer>().enabled = true; }
            catch { }
        }

        for (int i = 0; i < goggleobjs.Length; ++i)
        {

            try { goggleobjs[i].GetComponent<MeshRenderer>().enabled = false; }
            catch {}
            try { goggleobjs[i].GetComponent<SkinnedMeshRenderer>().enabled = false; }
            catch { }
        }
        
    }
}
