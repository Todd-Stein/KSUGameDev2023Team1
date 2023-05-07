using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_inventory : MonoBehaviour
{
    private GameObject[] inventory = new GameObject[5];
    private int slots = 5;

    [SerializeField]
    private GameObject[] ui;

    private void Start()
    {
        foreach(GameObject obj in ui)
        {
            obj.SetActive(false);
        }
    }

    public void Collect(GameObject obj)
    {
        string objName = obj.name;
        int slotIndex = int.Parse(objName.Substring(objName.Length - 2));

        if (slotIndex < 1 || slotIndex > slots)
        {
            Debug.LogWarning("Object " + objName + " has an invalid slot index.");
            return;
        }

        inventory[slotIndex - 1] = obj;
        ui[slotIndex - 1].SetActive(true);
        obj.SetActive(false);
    }

    public bool Use()
    {
        //This method returns a bool. true if inventory had a key in it. false if it did not.

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                inventory[i] = null;
                Debug.Log("Used key " + i);
                ui[i].SetActive(false);

                return true;
            }
        }
        return false;
    }
}
