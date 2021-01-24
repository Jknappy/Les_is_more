using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_health : MonoBehaviour
{
    public Text player_health_amount;
    public float starting_health;
    public float player_health;

    public bool has_been_damaged = false;

    public Respawn respawn;
    public GameObject spawn_point;

    // Start is called before the first frame update
    void Start()
    {
        respawn = GetComponent<Respawn>();
        player_health = starting_health;
    }

    // Update is called once per frame
    void Update()
    {
        if(has_been_damaged)
        {
            player_health -= 1f;
            has_been_damaged = false; 
        }

        if(player_health <= 0)
        {
            Restart();         
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "enemy")
        {
            has_been_damaged = true;
        }
    }
    public void Restart()
    {
        this.transform.position = spawn_point.transform.position;
        player_health = starting_health;
    }
}
