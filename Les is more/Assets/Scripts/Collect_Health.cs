using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect_Health : MonoBehaviour
{
    public Player_health ph;
    public GameObject player;
    public SpriteRenderer sp;
    public CircleCollider2D heart_collider;
    public AudioSource collect_heart_sound;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //ph = player.GetComponent<Player_health>();
    }

    private void Update()
    {
        if (ph.has_restarted)
        {
            sp.enabled = true;
            heart_collider.enabled = true; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {           
            sp.enabled = false;
            heart_collider.enabled = false;
            collect_heart_sound.Play();
            if(ph.starting_health != ph.player_health)
            {
                ph.player_health += 1f;
            }
        }
    }
}
