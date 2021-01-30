using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Les_animations : MonoBehaviour
{
    [Header("Les")]
    public bool is_les;
    public float les_health_amount;
    public float les_move_speed;
    public float les_jump_force;
    public float les_x_axis_knock_back;
    public float les_y_axis_knock_back;

    [Header("Sleepy")]
    public bool is_sleepy;
    public float sleepy_health_amount;
    public float sleepy_air_speed;
    public float sleepy_move_speed;
    public float sleepy_jump_force;
    public bool sleepy_getting_up;
    public float sleepy_x_axis_knock_back;
    public float sleepy_y_axis_knock_back;

    [Header("Angry")]
    public bool is_angry;
    public float angry_health_amount;
    public float angry_move_speed;
    public float angry_jump_force;
    public float angry_x_axis_knock_back;
    public float angry_y_axis_knock_back;
    public bool angry_landing;

    public bool angry_attacking;
    public float dash_attack_cooldown_time = 2f;
    public float dash_attack_cooldown;

    public float dash_attack_length;
    public float dash_attack_count;
    public bool dash_right;
    public float x_axis_dash_force;

    [Header("Components")]
    public Animator les_anim;
    public Animator sleepy_anim;
    public Animator angry_anim;
    //public Animator angry_anim;

    public PlayerMovement pm;
    public Player_health ph;
    public Les_attack la;

    public bool facing_right = true;

    public AudioSource dash_attack_sound;

    // Start is called before the first frame update
    void Start()
    {
        if (is_les)
        {
            pm.move_speed = les_move_speed;
            pm.jump_force = les_jump_force;

            pm.x_axis_knock_back = les_x_axis_knock_back;
            pm.y_axis_knock_back = les_y_axis_knock_back;

            ph.starting_health = les_health_amount;
            les_anim = GetComponent<Animator>();
        }
        else if(is_sleepy)
        {
            pm.move_speed = sleepy_move_speed;
            pm.jump_force = sleepy_jump_force;

            pm.x_axis_knock_back = sleepy_x_axis_knock_back;
            pm.y_axis_knock_back = sleepy_y_axis_knock_back;

            ph.starting_health = sleepy_health_amount;
            sleepy_anim = GetComponent<Animator>();
        }
        else if (is_angry)
        {
            pm.move_speed = angry_move_speed;
            pm.jump_force = angry_jump_force;

            pm.x_axis_knock_back = angry_x_axis_knock_back;
            pm.y_axis_knock_back = angry_y_axis_knock_back;

            ph.starting_health = angry_health_amount;
            angry_anim = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //need to set conditions for the triggers 
        if (is_les)
        {
            Les_Jump();
            Les_Falling();
            Les_Running();
            Flip_Les();
        }
        else if (is_sleepy)
        {
            Sleepy_Jump();
            Sleepy_Running();
            Flip_Sleepy();

            if (sleepy_anim.GetCurrentAnimatorStateInfo(0).IsName("sleepLanding")||
                sleepy_anim.GetCurrentAnimatorStateInfo(0).IsName("sleepAttack"))
            {
                sleepy_getting_up = true;
                pm.move_speed = 0;
            }
            else if(pm.is_grounded == false)
            {
                pm.move_speed = sleepy_air_speed;
            }
            else 
            {
                sleepy_getting_up = false;
                pm.move_speed = sleepy_move_speed;
            }
        }
        else if (is_angry)
        {
            Angry_Jump();
            Angry_Falling();
            Angry_Running();
            Flip_Angry();

            if(angry_anim.GetCurrentAnimatorStateInfo(0).IsName("angryLanding") && pm.is_grounded)
            {
                angry_landing = true;
                pm.move_speed = 0;
            }
            else
            {
                angry_landing = false;
                pm.move_speed = angry_move_speed;
            }
        }
    }

    void FixedUpdate()
    {
        if (dash_attack_count >= 0)
        {
            if (facing_right && angry_attacking)
            {
                
                dash_right = true;
                pm.rb.AddForce(new Vector2(x_axis_dash_force, 0));
                pm.rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                dash_attack_count -= Time.deltaTime;
            }
            if (!facing_right && angry_attacking)
            {
                dash_right = false;
                pm.rb.AddForce(new Vector2(-x_axis_dash_force, 0));
                pm.rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                dash_attack_count -= Time.deltaTime;
            }
        }
        else
        {
            pm.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            angry_attacking = false;
        }

        if (angry_attacking == true)
        {
            dash_attack_cooldown = dash_attack_cooldown_time;
        }
        else
        {
            dash_attack_cooldown -= Time.deltaTime;
        }

        //if ()
        //{
        //    dash_attack_sound.Play();
        //}
    }

    public void Recoil()
    {
        if (is_les)
        {
            les_anim.SetTrigger("Recoil");
        }
        if (is_sleepy)
        {
            sleepy_anim.SetTrigger("Recoil");
        }
        if (is_angry)
        {
            angry_anim.SetTrigger("Recoil");
        }
    }

    //les 
    public void Attack()
    {
        if (is_les)
        {
            Les_Attack();
        }
        else if (is_sleepy && !sleepy_anim.GetCurrentAnimatorStateInfo(0).IsName("sleepAttack")&& pm.is_grounded)
        {
            Sleepy_Attack();
        }
        else if (is_angry)
        {

            Angry_Attack();
        }
    }

    public void Les_Idle()
    {
        if (ph.has_restarted)
        {
            les_anim.SetTrigger("Idle");
        }
    }

    public void Les_Running()
    {
        if (pm.move_direction == 0)
        {
            les_anim.SetBool("Running", false);
        }
        else
        {
            les_anim.SetBool("Running", true);
        }
    }

    public void Les_Take_Off()
    {
        if (pm.is_jumping)
        {
            les_anim.SetTrigger("TakeOff");
        }
    }

    public void Les_Jump()
    {
        if (pm.is_grounded == true)
        {
            les_anim.SetBool("Jumping", false);
        }
        else
        {
            les_anim.SetBool("Jumping", true);
        }
    }

    public void Les_Falling()
    {
        if (pm.is_jumping == false && pm.is_grounded == false)
        {
            les_anim.SetBool("Falling", true);
        }
        else
        {
            les_anim.SetBool("Falling", false);
        }
    }

    public void Flip_Les()
    {
        if (pm.move_direction > 0 && !facing_right)
        {
            Les_Flip_Sprite();
        }
        else if (pm.move_direction < 0 && facing_right)
        {
            Les_Flip_Sprite();
        }
    }

    public void Les_Flip_Sprite()
    {
        facing_right = !facing_right;
        transform.Rotate(0f, 180f, 0f);
    }

    public void Les_Attack()
    {
        les_anim.SetTrigger("Attack");

        if (pm.is_grounded)
        {
            
            pm.move_speed = 0f;
            StartCoroutine(Les_Attack_Stall());
        }

        IEnumerator Les_Attack_Stall()
        {
            yield return new WaitForSeconds(.5f);
            pm.move_speed = les_move_speed;
        }
    }


    //sleepy 

    public void Sleepy_Attack()
    {
        sleepy_anim.SetTrigger("Attack");

        if (pm.is_grounded)
        {
            pm.move_speed = 0f;
            StartCoroutine(Sleepy_Attack_Stall());
        }
        
        IEnumerator Sleepy_Attack_Stall()
        {
            yield return new WaitForSeconds(2f);
            pm.move_speed = sleepy_move_speed;
        }
    }

    public void Sleepy_Jump()
    {
        if (pm.is_grounded == true)
        {
            sleepy_anim.SetBool("Jumping", false);

        }
        else
        {
            sleepy_anim.SetBool("Jumping", true);
            
        }
    }

    public void Sleepy_Running()
    {
        if (pm.move_direction == 0)
        {
            sleepy_anim.SetBool("Running", false);
        }
        else
        {
            sleepy_anim.SetBool("Running", true);
        }
    }

    public void Flip_Sleepy()
    {
        if (pm.is_grounded)
        {
            if (pm.move_direction > 0 && !facing_right)
            {
                Sleepy_Flip_Sprite();
            }
            else if (pm.move_direction < 0 && facing_right)
            {
                Sleepy_Flip_Sprite();
            }
        }
    }

    public void Sleepy_Flip_Sprite()
    {
        facing_right = !facing_right;
        transform.Rotate(0f, 180f, 0f);
    }



    public void Angry_Attack()
    {
        if(dash_attack_cooldown <= 0)
        {
            angry_attacking = true;
            dash_attack_count = dash_attack_length;
            angry_anim.SetTrigger("Attack");
        } 
    }

    public void Angry_Running()
    {
        if (pm.move_direction == 0)
        {
            angry_anim.SetBool("Running", false);
        }
        else
        {
            angry_anim.SetBool("Running", true);
        }
    }

    public void Flip_Angry()
    {
        if (!angry_attacking)
        {
            if (pm.move_direction > 0 && !facing_right)
            {
                Angry_Flip_Sprite();
            }
            else if (pm.move_direction < 0 && facing_right)
            {
                Angry_Flip_Sprite();
            }
        }       
    }

    public void Angry_Flip_Sprite()
    {
        facing_right = !facing_right;
        transform.Rotate(0f, 180f, 0f);
    }

    public void Angry_Jump()
    {
        if (pm.is_grounded == true)
        {
            angry_anim.SetBool("Jumping", false);
        }
        else
        {
            angry_anim.SetBool("Jumping", true);
        }
    }

    public void Angry_Falling()
    {
        if (pm.is_jumping == false && pm.is_grounded == false)
        {
            angry_anim.SetBool("Falling", true);
        }
        else
        {
            angry_anim.SetBool("Falling", false);
        }
    }

    public void Angry_Take_Off()
    {
        if (pm.is_jumping)
        {
            les_anim.SetTrigger("TakeOff");
        }
    }
}
