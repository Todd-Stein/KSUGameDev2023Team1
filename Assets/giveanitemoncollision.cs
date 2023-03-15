using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveanitemoncollision : MonoBehaviour
{
    private player_controls player;


    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            player = collision.gameObject.GetComponent<player_controls>();
            player.Pickup(gameObject);
        }

    }
}
