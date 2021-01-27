using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    //[HideInInspector]
    public float move_speed;
    //[HideInInspector]
    public float jump_force;

    [Header("Player Motion")]
    public float move_direction;
    public bool is_jumping = false;
    public bool is_grounded;

    [Header("KnockBack")]
    public float knock_back_length;
    public float knock_back_count;
    public bool knock_right;
    public float x_axis_knock_back;
    public float y_axis_knock_back;

    [Header("Collision Checking")]
    public Transform ceiling_check;
    public Transform ground_check;
    public LayerMask ground_object;
    public float check_radius;

    private Les_animations les_Animations;
    private Rigidbody2D rb;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        les_Animations = GetComponent<Les_animations>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        is_grounded = Physics2D.OverlapCircle(ground_check.position, check_radius, ground_object);
        
        Move();
    }

    private void Move()
    {
        //if player is not recieving knock back move
        if (knock_back_count <= 0)
        {
            rb.velocity = new Vector2(move_direction * move_speed, rb.velocity.y);
            if (is_jumping)
            {
                rb.AddForce(new Vector2(0f, jump_force));
                is_jumping = false;              
            }
        }
        else
        {
            if (knock_right)
            {
                //the number at the end is the y value for knock back amount
                rb.velocity = new Vector2(-x_axis_knock_back, y_axis_knock_back);
                knock_back_count -= Time.deltaTime;
            }
            if (!knock_right)
            {
                rb.velocity = new Vector2(x_axis_knock_back, y_axis_knock_back);
                knock_back_count -= Time.deltaTime;
            }
        }
    }

    private void ProcessInputs()
    {
        //set up switch statements for each player
        //process inputs
        move_direction = Input.GetAxis("Horizontal");


        if (Input.GetButtonDown("Jump") && is_grounded)
        {
            if (les_Animations.is_sleepy)
            {
                if (les_Animations.sleepy_getting_up)
                {
                    is_jumping = false;
                }
                else
                {
                    is_jumping = true;
                }
            }
            else //is les
            {
                is_jumping = true;
            }           
        }
    }
}
