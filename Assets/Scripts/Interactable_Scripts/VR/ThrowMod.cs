using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ThrowMod : MonoBehaviour
{
    public void Throwit()
    {
        if (GetComponent<DistractItem>() != null)
            GetComponent<DistractItem>().thrown = true;
    }
}
