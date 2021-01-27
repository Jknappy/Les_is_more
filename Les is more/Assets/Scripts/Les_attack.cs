using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Les_attack : MonoBehaviour
{
    //public GameObject weapon;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //weapon.GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //attack
            animator.SetTrigger("Attack");
            //Attack();
        }
    }

    void Attack()
    {
        //animator.SetTrigger("Attack");
        //Debug.Log("attack");
        // play an attak animation
        // detect enemies in range of attack
        // damage enemies
    }
}
