using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_health : MonoBehaviour
{
    [Header("Player Health")]
    public float starting_health;
    public float player_health;
    public float number_of_hearts;
    public float restart_count = 0f;

    public Image[] hearts;
    public Sprite full_heart;
    //public Sprite empty_heart;

    [Header("Display Text")]
    public Text player_health_amount;
    public Text restart_count_text;

    [Header("Spawn Location")]
    public GameObject spawn_point;

    [Header("Conditions")]
    public bool can_be_damaged = false;
    public bool has_restarted;
    public bool reached_goal = false;

    private Les_animations les_anim;
    private PlayerMovement pm;
    public bool respawn_enemies = false;

    // Start is called before the first frame update
    void Start()
    {
        les_anim = GetComponent<Les_animations>();
        pm = GetComponent<PlayerMovement>();
        player_health = starting_health;
        number_of_hearts = player_health;
    }

    // Update is called once per frame
    void Update()
    {
        number_of_hearts = player_health;

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < number_of_hearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if(can_be_damaged)
        {
            player_health -= 1f;
            can_be_damaged = false;
            //set coroutine for invincibility 
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
            les_anim.Recoil();

            pm.knock_back_count = pm.knock_back_length;
            can_be_damaged = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "goal")
        {
            reached_goal = true;
            //move to next level 
        }

        if(collision.transform.tag == "death")
        {
            player_health = 0f;
        }
    }

    public void Restart()
    {
        has_restarted = true;
        restart_count++;
        this.transform.position = spawn_point.transform.position;
        player_health = starting_health;

        // a timer to get all the enemies respawning, without it the true false check was too quick 
        StartCoroutine(Wait_For_Enemy_Respawn());    
    }

    IEnumerator Wait_For_Enemy_Respawn()
    {
        yield return new WaitForSeconds(.2f);
        has_restarted = false;
    }

}
