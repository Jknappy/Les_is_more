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

    public float move_speed;

    public Rigidbody2D rb;
    public float knock_back;
    public float knock_back_length;
    public float knock_back_count;
    public bool knock_right;

    public Transform target;
    public float target_range;

    public SpriteRenderer sp;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        health = starting_health;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        target = GameObject.FindWithTag("Player").transform;
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

            anim.SetTrigger("Recoil");
            knock_back_count = knock_back_length;
            //hit animation
            //recoil motion
            //move_speed = 0 
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
        //change, this was making it so only one enemy respawn
        //ph.has_restarted = false;
    }
}
