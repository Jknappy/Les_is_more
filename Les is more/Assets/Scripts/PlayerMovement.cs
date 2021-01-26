using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float move_speed;
    public float jump_force;
    public Transform ceiling_check;
    public Transform ground_check;
    public LayerMask ground_object;
    public float check_radius;

    public Animator anim;
    //public int max_jump_count;

    private Rigidbody2D rb;
    private bool facing_right = true;
    private float move_direction;
    private bool is_jumping = false;
    private bool is_grounded;
    //private int jump_count;
   // private bool initial_jump;

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
        //if (is_grounded)
        //{
        //    jump_count = max_jump_count;
        //}

        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(move_direction * move_speed, rb.velocity.y);
        if (is_jumping)
        {
            rb.AddForce(new Vector2(0f, jump_force));

            //jump_count--;
        }
        is_jumping = false;
    }

    private void ProcessInputs()
    {
        //process inputs
        move_direction = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && is_grounded)
        {
            anim.SetTrigger("TakeOff");
            is_jumping = true;
            //StartCoroutine(TakeOff());
            //IEnumerator TakeOff()
            //{
            //    yield return new WaitForSeconds(.02f);
            //    is_jumping = true;
            //}
                      
        }

        if (is_grounded == true)
        {
            anim.SetBool("Jumping", false);
        }
        else
        {
            anim.SetBool("Jumping", true);
        }
    }

    private void Animate()
    {

        //animate
        if (move_direction > 0 && !facing_right)
        {
            FlipCharacter();
        }
        else if (move_direction < 0 && facing_right)
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
