using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject spawn_point;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "death")
        {
            this.transform.position = spawn_point.transform.position;
        }
    }
}
