using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Les_animations : MonoBehaviour
{
    public Animator les_anim;
    public Animator sleepy_anim;

    public bool is_les;
    public bool is_sleepy;
    public bool is_angry;

    public PlayerMovement pm;
    public Player_health ph;
    public Les_attack la;

    public bool facing_right = true;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //need to set conditions for the triggers 
        if (is_les)
        {
            Les_Jump_Anim();
            Les_Falling();
            Les_Running();
            Flip_Les();
        }     
    }

    public void Attack()
    {
        if (is_les)
        {
            Les_Attack();
        }
        if (is_sleepy)
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

    public void Les_Jump_Anim()
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
        les_anim.SetTrigger("Recoil");
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

    public void Sleepy_Attack()
    {
        les_anim.SetTrigger("Attack");

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
}
