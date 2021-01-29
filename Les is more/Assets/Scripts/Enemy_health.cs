using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_health : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float starting_health;
    public float health;
    public float enemy_move_speed;
    private float move_speed;
    public float target_range;
    public bool is_in_purgatory = false;
    public float attack_stall_time = 1.5f;
    public float attack_stall;

    [Header("Locations")]
    public GameObject purgatory;   
    public GameObject spawn_point;
    public GameObject death_anim;

    [Header("KnockBack")]
    public float knock_back;
    public float knock_back_length;
    public float knock_back_count;
    public bool knock_right;

    [Header("Player Components")]
    public Transform target;
    public Player_health ph;
    public bool hit_player;

    [Header("Enemy Components")]
    public Rigidbody2D rb;
    public CircleCollider2D hitbox;
    public SpriteRenderer sp;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        health = starting_health;

        move_speed = enemy_move_speed;

        //sp = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        target = GameObject.FindWithTag("Player").transform;
        Generate_Spawn_point();    
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Purgatory();            
        }

        if (ph.has_restarted)
        {
            Respawn();
        }

        if(knock_back_count <= 0)
        {
            if(Vector2.Distance(transform.position, target.position) <= target_range)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, move_speed * Time.deltaTime);
                if (target.position.x >= transform.position.x)
                {
                    sp.flipX = true;
                }
                else
                {
                    sp.flipX = false;
                }
            }
        }
        else
        {
            if (knock_right)
            {
                rb.velocity = new Vector2(-knock_back, 0);
                knock_back_count -= Time.deltaTime;               
            }
            if (!knock_right)
            {
                rb.velocity = new Vector2(knock_back, 0);
                knock_back_count -= Time.deltaTime;               
            }
        }

        if (hit_player)
        {
            attack_stall -= Time.deltaTime;
        }

        if (attack_stall <= 0)
        {
            move_speed = enemy_move_speed;
            hit_player = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "attack")
        {
            health--;

            if(target.position.x > transform.position.x)
            {
                knock_right = true;
            }
            else
            {
                knock_right = false;
            }

            if(health != 0)
            {
                anim.SetTrigger("Recoil");
            }

            knock_back_count = knock_back_length;
            //move_speed = 0 
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player" && attack_stall <= 0)
        {
            attack_stall = attack_stall_time;
            move_speed = 0;
            hit_player = true;
        }

    }

    void Generate_Spawn_point()
    {
        GameObject spawn_location = Instantiate(spawn_point, transform.position, transform.rotation);
        spawn_point = spawn_location;
    }

    void Purgatory()
    {
        death_anim.SetActive(true);
        //Instantiate(death_anim, transform.position, transform.rotation);
        anim.enabled = false;
        sp.enabled = false;
        is_in_purgatory = true;
        hitbox.enabled = false;
        //this.transform.position = purgatory.transform.position;
    }

    void Respawn()
    {
        death_anim.SetActive(false);
        health = starting_health;
        anim.enabled = true;
        sp.enabled = true;
        hitbox.enabled = true;
        this.transform.position = spawn_point.transform.position;
        is_in_purgatory = false;
    }
}
