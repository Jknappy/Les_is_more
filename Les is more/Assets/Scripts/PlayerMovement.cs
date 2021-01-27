using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool is_les;
    public bool is_sleepy;
    public bool is_angry;

    //sleepy
    public bool has_jumped;
    public Les_attack la;

    public float move_speed;
    public float jump_force;
    public Transform ceiling_check;
    public Transform ground_check;
    public LayerMask ground_object;
    public float check_radius;
    public Animator anim;
 
    public float knock_back;
    public float knock_back_length;
    public float knock_back_count;
    public bool knock_right;

    private Rigidbody2D rb;
    private bool facing_right = true;
    private float move_direction;
    private bool is_jumping = false;
    public bool is_grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //jump_count = max_jump_count;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        if(move_direction == 0)
        {
            anim.SetBool("Running", false);
        }
        else
        {
            anim.SetBool("Running", true);
        }

        Animate();


    }

    private void FixedUpdate()
    {
        is_grounded = Physics2D.OverlapCircle(ground_check.position, check_radius, ground_object);
        
        Move();
    }

    private void Move()
    {
        if (knock_back_count <= 0)
        {
            rb.velocity = new Vector2(move_direction * move_speed, rb.velocity.y);
            if (is_jumping)
            {
                rb.AddForce(new Vector2(0f, jump_force));
                is_jumping = false;
                //jump_count--;
            }
        }
        else
        {
            if (knock_right)
            {
                rb.velocity = new Vector2(-knock_back, 1);
                knock_back_count -= Time.deltaTime;
            }
            if (!knock_right)
            {
                rb.velocity = new Vector2(knock_back, 1);
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
            anim.SetTrigger("TakeOff");
            is_jumping = true;                    
        }

        //les
        if (is_grounded == true && is_les)
        {
            anim.SetBool("Jumping", false);
        }
        else
        {
            anim.SetBool("Jumping", true);
        }

        //sleepy
        if (is_grounded == true && is_sleepy)
        {
            anim.SetBool("Jumping", false);
        }
        else
        {
            anim.SetBool("Jumping", true);
            //sleepy cant attack
            has_jumped = true;
            if (is_grounded)
            {
                StartCoroutine (Sleepy_cant_move_attack());
            }            
        }

        IEnumerator Sleepy_cant_move_attack()
        {
            if(has_jumped == true && is_grounded == true)
            {
                //TO DO
                //sleepy cant move or jump
                la.can_attack = false;
                yield return new WaitForSeconds(1.5f);
            }
            la.can_attack = true;
            has_jumped = false;
        }

        //les
        //if (is_les && is_jumping == false && is_grounded == false)
        //{
        //    anim.SetBool("Falling", true);
        //}
        //else
        //{
        //    anim.SetBool("Falling", false);
        //}
    }

    private void Animate()
    {
        //les
        if (move_direction > 0 && !facing_right && is_les)
        {
            FlipCharacter();
        }
        else if (move_direction < 0 && facing_right && is_les)
        {
            FlipCharacter();
        }

        //sleepy
        if (move_direction > 0 && !facing_right && is_sleepy && is_grounded)
        {
            FlipCharacter();
        }
        else if (move_direction < 0 && facing_right && is_sleepy && is_grounded)
        {
            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        facing_right = !facing_right;
        transform.Rotate(0f, 180f, 0f);
    }
}
