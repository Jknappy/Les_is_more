using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_health : MonoBehaviour
{
    public float starting_health;
    public float health;
    public bool is_in_purgatory = false;
    public CircleCollider2D hitbox;

    public GameObject purgatory;   
    public GameObject spawn_point;

    public Player_health ph;

    // Start is called before the first frame update
    void Start()
    {
        health = starting_health;

        Generate_Spawn_point();    
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Purgatory();
            //death animation
        }

        if (ph.has_restarted)
        {           
            Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "attack")
        {
            health--;
            //hit animation
            //recoil motion 
        }
    }

    void Generate_Spawn_point()
    {
        GameObject spawn_location = Instantiate(spawn_point, transform.position, transform.rotation);
        spawn_point = spawn_location;
    }

    void Purgatory()
    {
        is_in_purgatory = true;
        hitbox.enabled = false;
        this.transform.position = purgatory.transform.position;
    }

    void Respawn()
    {       
        is_in_purgatory = false;
        hitbox.enabled = true;
        health = starting_health;
        this.transform.position = spawn_point.transform.position;
        ph.has_restarted = false;
    }
}
