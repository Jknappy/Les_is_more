using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float starting_health;

    public float movement_scalar;
    public float max_speed;

    public float jump_scalar;

    private bool is_on_ground;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x_movement = Input.GetAxis("Horizontal");

        if(rb.velocity.magnitude < max_speed)
        {
            Vector2 movement = new Vector2(x_movement, 0);
            rb.AddForce(movement * movement_scalar);
        }

        if(Input.GetButtonDown("Jump") && is_on_ground)
        {
            Vector2 jump_force = new Vector2(0, jump_scalar);
            rb.AddForce(jump_force);
            //jump
        }
        //Input.GetAxis("Vertical");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(CollisionIsWithGround(collision))
        {
            is_on_ground = true;
            Debug.Log(is_on_ground);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(!CollisionIsWithGround(collision))
        {
            is_on_ground = false;
            Debug.Log(is_on_ground);
        }
    }

    private bool CollisionIsWithGround(Collision2D collision)
    {
        bool is_with_ground = false;
        foreach(ContactPoint2D c in collision.contacts)
        {
            Vector2 collision_direction_vector = c.point - rb.position;
            if(collision_direction_vector.y < 0)
            {
                is_with_ground = true;
            }
        }

        return is_with_ground;
    }
}
//for whatever reason the movement here is not snappy and it doesnt always work
//the jump hieght varies and the jump itself doesnt always work