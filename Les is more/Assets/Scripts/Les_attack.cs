using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Les_attack : MonoBehaviour
{
    //public GameObject weapon;
    public Les_animations les_anim;

    public PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        les_anim = GetComponent<Les_animations>();
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
            les_anim.Attack();
        }
    }
}
