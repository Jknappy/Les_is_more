using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Les_attack : MonoBehaviour
{
    //public GameObject weapon;
    public Les_animations anim;

    public PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Les_animations>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.Attack();
        }
    }
}
