using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Les_attack : MonoBehaviour
{
    //public GameObject weapon;
    public Animator animator;
    public PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        //weapon.GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && pm.is_les)
        {
            animator.SetTrigger("Attack");
            if (pm.is_grounded)
            {
                pm.move_speed = 0f;
                StartCoroutine(Les_Attack_Stall());
            }
        }
        IEnumerator Les_Attack_Stall()
        {
            yield return new WaitForSeconds(.5f);
            pm.move_speed = 1.5f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && pm.is_sleepy)
        {
            animator.SetTrigger("Attack");
            if (pm.is_grounded)
            {
                pm.move_speed = 0f;
                StartCoroutine(Sleepy_Attack_Stall());
            }
        }
        IEnumerator Sleepy_Attack_Stall()
        {
            yield return new WaitForSeconds(2f);
            pm.move_speed = .75f;
        }
    }
}
