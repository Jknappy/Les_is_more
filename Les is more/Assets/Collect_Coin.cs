using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect_Coin : MonoBehaviour
{
    public Player_health ph;
    public GameObject player;
    public SpriteRenderer sp;
    public CircleCollider2D coin_collider;

    public bool collected_coin;

    private void Start()
    {       
        //player = GameObject.FindWithTag("Player");
        //ph = player.GetComponent<Player_health>();
    }

    private void Update()
    {
        if (ph != null && ph.has_restarted)
        {
            sp.enabled = true;
            coin_collider.enabled = true;
            ph.player_collected_coin = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sp.enabled = false;
            coin_collider.enabled = false;

            ph.player_collected_coin = true;
            ph.player_coin_count++;
        }
            ph.player_collected_coin = false;
        
    }
}
