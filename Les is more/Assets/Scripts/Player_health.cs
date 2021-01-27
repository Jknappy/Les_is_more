using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_health : MonoBehaviour
{
    public Text player_health_amount;
    public float starting_health;
    public float player_health;

    public Les_animations les_anim;

    public bool has_been_damaged = false;
    public bool has_restarted;
    public bool reached_goal = false;

    public bool respawn_enemies = false;

    public Respawn respawn;
    public GameObject spawn_point;

    public PlayerMovement pm;
    public Animator anim;

    public float restart_count = 0f;

    // Start is called before the first frame update
    void Start()
    {
        les_anim = GetComponent<Les_animations>();

        anim = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
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

        if(player_health <= 0|| Input.GetKeyDown(KeyCode.R))
        {
            Restart();            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "enemy")
        {
            if (transform.position.x < collision.transform.position.x)
            {
                pm.knock_right = true;
            }
            else
            {
                pm.knock_right = false;
            }
            les_anim.Les_Recoil();
            pm.knock_back_count = pm.knock_back_length;
            has_been_damaged = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "goal")
        {
            reached_goal = true;
            //move to next level 
        }
    }

    public void Restart()
    {
        has_restarted = true;
        restart_count++;
        this.transform.position = spawn_point.transform.position;
        player_health = starting_health;

        les_anim.Les_Idle();

        //dumb way to do this but lets see if it works 
        StartCoroutine(Wait_For_Enemy_Respawn());    
    }

    IEnumerator Wait_For_Enemy_Respawn()
    {
        yield return new WaitForSeconds(.2f);
        has_restarted = false;
    }

}
