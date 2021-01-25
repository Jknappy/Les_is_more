using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{   
    public Player_health ph;

    private void Start()
    {
        ph = GetComponent<Player_health>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "death")
        {
            ph.player_health = 0;
        }
    }
}
