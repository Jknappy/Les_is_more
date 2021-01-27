using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Les_animations : MonoBehaviour
{
    public Animator les_anim;
    public Animator sleepy_anim;

    public bool is_les;
    public float les_move_speed;

    public bool is_sleepy;
    public bool sleepy_getting_up;
    public float sleepy_move_speed;

    public bool is_angry;

    public PlayerMovement pm;
    public Player_health ph;
    public Les_attack la;

    public bool facing_right = true;

    // Start is called before the first frame update
    void Start()
    {
        if (is_les)
        {
            pm.move_speed = les_move_speed;
            les_anim = GetComponent<Animator>();
        }
        else if(is_sleepy)
        {
            pm.move_speed = sleepy_move_speed;
            sleepy_anim = GetComponent<Animator>();
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
            else
            {
                sleepy_getting_up = false;
                pm.move_speed = sleepy_move_speed;
            }
        }
    }

    public void Attack()
    {
        if (is_les)
        {
            Les_Attack();
        }
        else if (is_sleepy)
        {
            Sleepy_Attack();
        }
        if (is_angry)
        {
            //Angry_Attack();
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

    public void Recoil()
    {
        if (is_les)
        {
            les_anim.SetTrigger("Recoil");
        }
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
            pm.move_speed = 1.5f;
        }
    }


    //sleepy animations

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
            pm.move_speed = .75f;
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
}
