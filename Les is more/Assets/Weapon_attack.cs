using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_attack : MonoBehaviour
{
    public Animation weapon_attack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //attack
            Attack();
        }
    }

    void Attack()
    {
        weapon_attack.Play();
    }
}   
