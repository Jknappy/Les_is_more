using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_Level : MonoBehaviour
{
    public bool go_next_level = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            go_next_level = true;
            Debug.Log("next level?");
        }
    }
}
