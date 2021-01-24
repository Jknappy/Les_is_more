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
    public int max_jump_count;

    private Rigidbody2D rb;
    private bool facing_right = true;
    private float move_direction;
    private bool is_jumping = false;
    private bool is_grounded;
    private int jump_count;
    private bool initial_jump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        jump_count = max_jump_count;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        Animate();
    }

    private void FixedUpdate()
    {
        is_grounded = Physics2D.OverlapCircle(ground_check.position, check_radius, ground_object);
        if (is_grounded)
        {
            jump_count = max_jump_count;
        }

        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(move_direction * move_speed, rb.velocity.y);
        if (is_jumping)
        {
            rb.AddForce(new Vector2(0f, jump_force));
            jump_count--;
        }
        is_jumping = false;
    }

    private void ProcessInputs()
    {
        //process inputs
        move_direction = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && jump_count > 0)
        {
            is_jumping = true;

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
